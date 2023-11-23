using LoCoMPro.Data;
using LoCoMPro.Models;
using LoCoMPro.Utils.Interfaces;
using LoCoMPro.ViewModels.Cuenta;
using Microsoft.EntityFrameworkCore;

namespace LoCoMPro.Utils.Buscadores
{
    // Buscador especializado para la pagina de ver favoritos
    public class BuscadorDeFavoritos : IBuscador<FavoritoVM>
    {
        // Contexto
        protected readonly LoCoMProContext contexto;

        // Usuario que realizó los aportes
        protected string? usuario { get; set; }

        // Constructor
        public BuscadorDeFavoritos(LoCoMProContext contexto, string? usuario = null)
        {
            this.contexto = contexto;
            this.usuario = usuario;
        }

        // Setters
        public void setUsuario(string? usuario)
        {
            this.usuario = usuario;
        }
  
        // Buscar favoritos del usuario
        public IQueryable<FavoritoVM> buscar()
        {
            if (this.usuario != null)
            {
                Usuario? usuario = this.contexto.Usuarios
                    .Include(u => u.favoritos)
                    .FirstOrDefault(u => u.nombreDeUsuario == this.usuario);

                if (usuario != null)
                {
                    return usuario.favoritos
                        .Select(f => new FavoritoVM
                        { 
                            nombreProducto = f.nombre,
                            nombreCategoria = f.nombreCategoria,
                            nombreMarca = f.marca == null ? f.marca : "Sin marca"
                        }).AsQueryable();
                } else
                {
                    return Enumerable.Empty<FavoritoVM>().AsQueryable();
                }
            }
            else
            {
                return Enumerable.Empty<FavoritoVM>().AsQueryable();
            }
        }
    }
}