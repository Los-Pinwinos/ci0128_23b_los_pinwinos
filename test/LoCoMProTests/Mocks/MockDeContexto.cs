using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LoCoMProTests.Mocks
{
    public class MockDeContexto<TipoContexto> : MockWrapper<TipoContexto> where TipoContexto : DbContext
    {
        public MockDeContexto() : base() { }

        // Agrega una instancia del modelo al conjunto de datos del Mock
        public void ConfigurarParaInstanciasDeModelo<TipoModelo>
            (Expression<Func<TipoContexto, DbSet<TipoModelo>>> expresionDeModeloEnContexto,
            DbSet<TipoModelo> instancias) where TipoModelo : class
        {

            this.mock.Setup(expresionDeModeloEnContexto).Returns(instancias);

        }
    }
}
