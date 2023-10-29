using Microsoft.Data.SqlClient;
using NuGet.Packaging;
using System.Data;
using System.Data.Common;
using System.Reflection.Metadata;

namespace LoCoMPro.Utils.SQL
{
    public class ControladorComandosSQL
    {
        private SqlConnection conexion { get; set; }

        private readonly IConfiguration configuracion;

        private string? nombreComando { get; set; }
        IList<SqlParameter> parametros { get; set; }

        public ControladorComandosSQL()
        {
            this.configuracion = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Abrir una conexion
            this.conexion = new SqlConnection(this.configuracion.GetConnectionString("LoCoMProContextRemote"));
            this.conexion.Open();

            this.parametros = new List<SqlParameter>();
        }

        ~ControladorComandosSQL()
        {
            this.conexion.Close();
        }

        public void ConfigurarNombreComando(string nombreComando)
        {
            this.nombreComando = nombreComando;
        }

        public void ConfigurarParametroComando(string nombreParametro, object valor)
        {
            this.parametros.Add(new SqlParameter("@" + nombreParametro, valor));
        }

        public IList<object[]> EjecutarFuncion()
        {
            SqlDataAdapter adaptador = new SqlDataAdapter();
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
                cadenaComando = cadenaComando.Substring(0, cadenaComando.Length - 2); // Remove the trailing comma and space
            }

            cadenaComando += ")";

            SqlCommand comando = new SqlCommand(cadenaComando, this.conexion);

            // Add the parameters to the SqlCommand
            foreach (SqlParameter parametro in parametros)
            {
                comando.Parameters.Add(parametro);
            }

            return this.llenarResultados(comando.ExecuteReader());
        }

        public IList<object[]> EjecutarProcedimiento()
        {
            SqlDataAdapter adaptador = new SqlDataAdapter();
            if (this.conexion.State == ConnectionState.Closed)
            {
                this.conexion.Open();
            }

            SqlCommand comando = new SqlCommand(nombreComando, this.conexion);
            comando.CommandType = CommandType.StoredProcedure;

            // Add the parameters to the SqlCommand
            foreach (SqlParameter parametro in parametros)
            {
                comando.Parameters.Add(parametro);
            }

            return this.llenarResultados(comando.ExecuteReader());
        }

        private IList<object[]> llenarResultados(SqlDataReader lector)
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
