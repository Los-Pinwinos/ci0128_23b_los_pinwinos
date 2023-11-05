using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace LoCoMProTests.Mocks
{
    public class MockDeModelo<TipoModelo> : MockWrapper<DbSet<TipoModelo>> where TipoModelo : class
    {
        // Conjunto de datos para el mock
        private IList<TipoModelo> datosMock { get; set; }
        private IQueryable<TipoModelo> instanciasConsultables { get; set; }

        public MockDeModelo() : base()
        {

            this.datosMock = new List<TipoModelo>();
            this.instanciasConsultables = datosMock.AsQueryable();

        }

        // Agrega una instancia del modelo al conjunto de datos del Mock
        public void AgregarInstanciaDeModelo(TipoModelo instancia)
        {

            this.datosMock.Add(instancia);

        }

        // Agrega un rango de instancias del modelo al conjunto de datos del Mock
        public void AgregarRangoInstanciasDeModelo(IEnumerable<TipoModelo> instancias)
        {

            this.datosMock.AddRange(instancias);

        }

        public void Configurar()
        {
            this.instanciasConsultables = this.datosMock.AsQueryable();

            this.mock.As<IQueryable<TipoModelo>>().Setup(m => m.Provider).Returns(this.instanciasConsultables.Provider);
            this.mock.As<IQueryable<TipoModelo>>().Setup(m => m.Expression).Returns(this.instanciasConsultables.Expression);
            this.mock.As<IQueryable<TipoModelo>>().Setup(m => m.ElementType).Returns(this.instanciasConsultables.ElementType);
            this.mock.As<IQueryable<TipoModelo>>().Setup(m => m.GetEnumerator()).Returns(() => this.instanciasConsultables.GetEnumerator());

        }
    }
}
