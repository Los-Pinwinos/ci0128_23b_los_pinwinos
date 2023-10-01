using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LoCoMPro
{
    public class ListaPaginada<T> : List<T>
    {
        public int IndicePagina { get; private set; }
        public int PaginasTotales { get; private set; }

        public ListaPaginada()
        {
            List<T> items = new List<T>();
            IndicePagina = 1;
            PaginasTotales = 0;
        }
        public ListaPaginada(List<T> items, int count, int pageIndex, int pageSize)
        {
            IndicePagina = pageIndex;
            PaginasTotales = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool TienePaginaPrevia => IndicePagina > 1;

        public bool TieneProximaPagina => IndicePagina < PaginasTotales;

        public static async Task<ListaPaginada<T>> CrearAsync(
            IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip(
                (pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            return new ListaPaginada<T>(items, count, pageIndex, pageSize);
        }
    }
}
