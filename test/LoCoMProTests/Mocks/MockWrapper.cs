using Moq;
using System.Linq.Expressions;


namespace LoCoMProTests.Mocks
{
    public class MockWrapper<TipoElemento> where TipoElemento : class
    {
        protected Mock<TipoElemento> mock { get; set; }

        public MockWrapper()
        {
            mock = new Mock<TipoElemento>();
        }

        public TipoElemento ObtenerObjetoDeMock()
        {

            return this.mock.Object;

        }

        // Verificar si una condicion se cumple dadas las acciones ejecutadas sobre el mock
        public void Verificar(Expression<Action<TipoElemento>> expresionVerificable)
        {

            this.mock.Verify(expresionVerificable);

        }

        public void Verificar(Expression<Action<TipoElemento>> expresionVerificable, Times objetoTimes)
        {

            this.mock.Verify(expresionVerificable, objetoTimes);

        }

        public void Verificar(Expression<Action<TipoElemento>> expresionVerificable, Times objetoTimes, string mensajeDeError)
        {

            this.mock.Verify(expresionVerificable, objetoTimes, mensajeDeError);

        }

    }
}
