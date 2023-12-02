using LoCoMPro.Data;
using LoCoMPro.Models;
using LoCoMPro.Utils.Buscadores;
using LoCoMPro.Utils.Interfaces;
using LoCoMPro.Utils;
using LoCoMPro.ViewModels.Moderacion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace LoCoMPro.Pages.Moderacion
{
    public class OutliersPrecioModel : PageModel
    {
        protected readonly LoCoMProContext contexto;
        protected readonly IConfiguration configuracion;
        public string? outliers { get; set; }
        public RegistroOutlierPrecioVM registrosVM { get; set; }
        public int paginaDefault { get; set; }
        public int resultadosPorPagina { get; set; }

        public OutliersPrecioModel(LoCoMProContext contexto, IConfiguration configuracion)
        {
            this.contexto = contexto;
            this.configuracion = configuracion;
            this.registrosVM = new RegistroOutlierPrecioVM
            {
                fecha = DateTime.Now,
                usuario = "",
                precio = 0,
                producto = "",
                tienda = "",
                provincia = "",
                canton = "",
                distrito = ""
            };
            this.paginaDefault = 1;
            this.resultadosPorPagina = this.configuracion.GetValue("TamPaginaOutliers", 8);
        }

        public IActionResult OnGet()
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated || !User.IsInRole("moderador"))
            {
                // Se redirige al usuario porque debe estar ingresado para esta funcionalidad
                ViewData["RedirectMessage"] = "moderador";
            }

            // Configurar buscador
            IBuscador<RegistroOutlierPrecioVM> buscador = new BuscadorDeOutliersPrecio(this.contexto);
            // Consultar la base de datos
            IQueryable<RegistroOutlierPrecioVM> busqueda = buscador.buscar();

            // Si la busqueda tuvo resultados
            List<RegistroOutlierPrecioVM> resultados = busqueda.ToList();
            if (resultados.Count != 0)
            {
                // Asignar data de JSON
                this.outliers = ControladorJson.ConvertirAJson(resultados);
            }
            else
            {
                this.outliers = "Sin resultados";
            }
            return Page();
        }

        public void OnGetEliminarRegistros(string registrosStr)
        {
            if (registrosStr != null)
            {
                List<RegistroEliminarVM> registros = JsonConvert.DeserializeObject<List<RegistroEliminarVM>>(registrosStr);
                if (registros != null && registros.Count > 0)
                {
                    try
                    {
                        for (int registro = 0; registro < registros.Count; ++registro)
                        {
                            // Obtener el registro de la base de datos
                            Registro? registroEliminar = contexto.Registros
                                .FirstOrDefault(r => r.creacion == registros[registro].fecha && r.usuarioCreador == registros[registro].usuario);
                            if (registroEliminar != null)
                            {  // Ocultar el registro
                                registroEliminar.visible = false;
                                this.contexto.SaveChanges();
                            }
                        }
                    } catch (Exception ex)
                    {
                        Console.Error.WriteLine("Error al intentar eliminar registros inválidos: " + ex);
                    }
                }
            }
        }
    }
}
