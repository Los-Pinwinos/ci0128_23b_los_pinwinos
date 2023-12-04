using LoCoMPro.Data;
using LoCoMPro.Models;
using LoCoMPro.Utils.Interfaces;
using LoCoMPro.ViewModels.Busqueda;
using LoCoMPro.ViewModels.Busqueda.Favoritos;
using Microsoft.EntityFrameworkCore;

namespace LoCoMPro.Utils.Buscadores
{

	// Buscador especializado para la pagina de busqueda de favoritos
	public class BuscadorDeProductosFavoritosEnTienda : IBuscador<ProductoFavoritoVM>
	{
		// Contexto
		protected readonly LoCoMProContext contexto;

		// Usuario con los favoritos
		private string? usuario { get; set; }
		private string? tienda { get; set; }
		private string? provincia { get; set; }
		private string? canton { get; set; }
		private string? distrito { get; set; }

		public BuscadorDeProductosFavoritosEnTienda(LoCoMProContext contexto, string? usuario = null, string? tienda = null, string? provincia = null, string? canton = null, string? distrito = null)
		{
			this.contexto = contexto;
			this.usuario = usuario;
			this.tienda = tienda;
			this.provincia = provincia;
			this.canton = canton;
			this.distrito = distrito;
		}

		// Setters
		public void SetUsuario(string? usuario)
		{
			this.usuario = usuario;
		}

		public void SetTienda(string? tienda)
		{
			this.tienda = tienda;
		}

		public void SetProvincia(string? provincia)
		{
			this.provincia = provincia;
		}

		public void SetCanton(string? canton)
		{
			this.canton = canton;
		}

		public void SetDistrito(string? distrito)
		{
			this.distrito = distrito;
		}

		// Buscar favoritos del usuario
		public IQueryable<ProductoFavoritoVM> buscar()
		{
			if (!string.IsNullOrEmpty(this.usuario) &&
				!string.IsNullOrEmpty(this.tienda) &&
				!string.IsNullOrEmpty(this.provincia) &&
				!string.IsNullOrEmpty(this.canton) &&
				!string.IsNullOrEmpty(this.distrito))
			{
				Usuario? usuario = this.contexto.Usuarios
					.Include(u => u.favoritos)
					.FirstOrDefault(u => u.nombreDeUsuario == this.usuario);

				// Obtener el nombre de los favoritos
				IList<string> favoritos = usuario?.favoritos.Select(f => f.nombre).ToList() ?? new List<string>();

				if (usuario != null && favoritos.Count > 0)
				{
					IQueryable<Registro> filtradoIQ = this.contexto.Registros
						.Include(r => r.producto)
						.Where(r => r.nombreTienda == this.tienda &&
									r.nombreProvincia == this.provincia &&
									r.nombreCanton == this.canton &&
									r.nombreDistrito == this.distrito &&
									favoritos.Contains(r.productoAsociado));

					// Si tiene resultados
					if (filtradoIQ.Count() > 0)
					{
						IQueryable<ProductoFavoritoVM> resultadosIQ = filtradoIQ
							.GroupBy(r => r.productoAsociado)
							.Select(grupo => new ProductoFavoritoVM
							{
								producto = grupo.Key,
								precio = grupo.OrderByDescending(r => r.creacion).FirstOrDefault()!.precio,
								unidad = grupo.FirstOrDefault()!.producto!.nombreUnidad,
								categoria = grupo.FirstOrDefault()!.producto!.nombreCategoria,
								marca = grupo.FirstOrDefault()!.producto!.marca ?? "Sin marca"
							});

						return resultadosIQ;
					}
					else
					{
						return Enumerable.Empty<ProductoFavoritoVM>().AsQueryable();
					}
				}
				else
				{
					return Enumerable.Empty<ProductoFavoritoVM>().AsQueryable();
				}
			}
			else
			{
				return Enumerable.Empty<ProductoFavoritoVM>().AsQueryable();
			}
		}
	}
}
