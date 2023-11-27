using Microsoft.Data.SqlClient;
using System.Data;

namespace LoCoMPro.Utils.SQL
{
    public class ControladorComandosSql
    {
        private SqlConnection conexion { get; set; }

        private string? nombreComando { get; set; }
        IList<SqlParameter> parametros { get; set; }

        public ControladorComandosSql()
        {
            IConfiguration configuracion = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Crea un encriptador para desencriptar el connection string
            Encriptador encriptador = new Encriptador();
            // Crea una constante para el connection string
            // TODO(Pinwinos): Sincronizar con la de program.cs
            const string connectionString = "LoCoMProContextRemote";

            // Abrir una conexion
            this.conexion = new SqlConnection(
                encriptador.desencriptar(
                    configuracion.GetConnectionString(connectionString) ??
                    throw new InvalidOperationException(
                        "Connection string " + connectionString + " no econtrada.")));
            this.conexion.Open();

            this.parametros = new List<SqlParameter>();
        }

        ~ControladorComandosSql()
        {
            this.conexion.Close();
        }

        public void reiniciarControlador()
        {
            this.nombreComando = null;
            this.limpiarParametros();
        }

        public void limpiarParametros()
        {
            this.parametros.Clear();
        }

        public void ConfigurarNombreComando(string nombreComando)
        {
            this.nombreComando = nombreComando;
        }

        public void ConfigurarParametroComando(string nombreParametro, object valor)
        {
            this.parametros.Add(new SqlParameter("@" + nombreParametro, valor));
        }

        public void ConfigurarParametroDateTimeComando(string nombreParametro, object valor)
        {
            var parametro = new SqlParameter("@" + nombreParametro, SqlDbType.DateTime2);
            parametro.Value = valor;
            System.Diagnostics.Debug.WriteLine("FECHA_CONFIGURADO: " + parametro.Value);
            this.parametros.Add(parametro);
        }

        public IList<object[]> EjecutarFuncion()
        {
            if (this.conexion.State == ConnectionState.Closed)
            {
                this.conexion.Open();
            }

            string cadenaComando = "SELECT dbo." + nombreComando + "(";

            for (int i = 0; i < parametros.Count; i++)
            {
                cadenaComando += parametros[i].ParameterName + ", ";
            }

            if (parametros.Count > 0)
            {
                // Remover la última coma y espacio
                cadenaComando = cadenaComando.Substring(0, cadenaComando.Length - 2);
            }

            cadenaComando += ")";

            SqlCommand comando = new SqlCommand(cadenaComando, this.conexion);

            // Agregar los parametros al comando
            foreach (SqlParameter parametro in parametros)
            {
                comando.Parameters.Add(parametro);
            }

            return LlenarResultados(comando.ExecuteReader());
        }

        public IList<object[]> EjecutarProcedimiento()
        {
            if (this.conexion.State == ConnectionState.Closed)
            {
                this.conexion.Open();
            }

            SqlCommand comando = new SqlCommand(nombreComando, this.conexion);
            comando.CommandType = CommandType.StoredProcedure;

            // Agregar los parametros al comando
            foreach (SqlParameter parametro in parametros)
            {
                comando.Parameters.Add(parametro);
            }

            return LlenarResultados(comando.ExecuteReader());
        }

        private static IList<object[]> LlenarResultados(SqlDataReader lector)
        {
            IList<object[]> resultados = new List<object[]>();

            while(lector.Read())
            {
                object[] array = new object[lector.GetColumnSchema().Count];
                lector.GetValues(array);
                resultados.Add(array);
            }

            return resultados;
        }
    }
}
