using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using LoCoMProTestFuncionales.PageModels.DetallesRegistros;
using LoCoMProTestFuncionales.PageModels.Busqueda;
using LoCoMProTestFuncionales.PageModels.Cuenta;
using LoCoMProTestFuncionales.PageModels.Home;
using LoCoMProTestFuncionales.PageModels.VerRegistros;

namespace LoCoMProFunctionalTests.Pages.DetallesRegistro
{
    [TestFixture]
    public class DetallesRegistro
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 3
        [Test]
        public void PaginaDetallesRegistro_RevisarCalificacion_DeberiaSerCero()
        {
            // Preparación __________________________________________________________________
            // Se realiza una búsqueda con "Iphone"
            PaginaHome paginaHome = new PaginaHome(this.driver);
            driver.Navigate().GoToUrl(paginaHome.ObtenerURL());
            PaginaBusqueda paginaBusqueda = paginaHome.Buscar("Iphone");
            // Se selecciona el primer resultado
            PaginaVerRegistros paginaRegistros = paginaBusqueda.SeleccionarResultado(0);

            // Acción _______________________________________________________________________
            // Se selecciona el primer registro
            PaginaDetallesRegistro paginaDetalles = paginaRegistros.SeleccionarResultado(0);

            // Verificación _________________________________________________________________
            string resultado = paginaDetalles.ObtenerCalificacion();
            Assert.That(resultado, Is.EqualTo("0,0 de 5,0"));
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 3
        [Test]
        public void PaginaDetallesRegistro_RevisarCantidadCalificaciones_DeberiaSerCero()
        {
            // Preparación __________________________________________________________________
            PaginaHome paginaHome = new PaginaHome(this.driver);
            driver.Navigate().GoToUrl(paginaHome.ObtenerURL());
            // Se realiza una búsqueda con "Iphone"
            PaginaBusqueda paginaBusqueda = paginaHome.Buscar("Iphone");
            // Se selecciona el primer resultado
            PaginaVerRegistros paginaRegistros = paginaBusqueda.SeleccionarResultado(0);

            // Acción _______________________________________________________________________
            // Se selecciona el primer registro
            PaginaDetallesRegistro paginaDetalles = paginaRegistros.SeleccionarResultado(0);

            // Verificación _________________________________________________________________
            string resultado = paginaDetalles.ObtenerCantidadCalificaciones();
            Assert.That(resultado, Is.EqualTo("(0)"));
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 3
        [Test]
        public void PaginaDetallesRegistro_RevisarMensajeHaCalificado_DeberiaSerCinco()
        {
            // Preparación __________________________________________________________________
            PaginaHome paginaHome = IniciarSesion();
            // Se realiza una búsqueda con "Iphone"
            PaginaBusqueda paginaBusqueda = paginaHome.Buscar("Iphone");
            // Se selecciona el primer resultado
            PaginaVerRegistros paginaRegistros = paginaBusqueda.SeleccionarResultado(0);

            // Acción _______________________________________________________________________
            // Se selecciona el quinto registro
            PaginaDetallesRegistro paginaDetalles = paginaRegistros.SeleccionarResultado(4);

            // Verificación _________________________________________________________________
            string resultado = paginaDetalles.ObtenerMensajeCalificacion();
            Assert.That(resultado, Is.EqualTo("Ha calificado con: 5"));
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 3
        [Test]
        public void PaginaDetallesRegistro_RevisarAlCalificarActualizarCalificacion_DeberiaSerUno()
        {
            // Preparación __________________________________________________________________
            PaginaHome paginaHome = IniciarSesion();
            // Se realiza una búsqueda con "Iphone"
            PaginaBusqueda paginaBusqueda = paginaHome.Buscar("Iphone");
            // Se selecciona el primer resultado
            PaginaVerRegistros paginaRegistros = paginaBusqueda.SeleccionarResultado(0);
            // Se selecciona el segundo registro
            PaginaDetallesRegistro paginaDetalles = paginaRegistros.SeleccionarResultado(2);

            // Acción _______________________________________________________________________
            // Se cambia la calificacion
            paginaDetalles.clickEstrella(1);

            // Verificación _________________________________________________________________
            string resultado = paginaDetalles.ObtenerCalificacion();
            Assert.That(resultado, Is.EqualTo("1,0 de 5,0"));
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 3
        [Test]
        public void PaginaDetallesRegistro_RevisarAlCalificarActualizarCantidad_DeberiaSerUno()
        {
            // Preparación __________________________________________________________________
            PaginaHome paginaHome = IniciarSesion();
            // Se realiza una búsqueda con "Iphone"
            PaginaBusqueda paginaBusqueda = paginaHome.Buscar("Iphone");
            // Se selecciona el primer resultado
            PaginaVerRegistros paginaRegistros = paginaBusqueda.SeleccionarResultado(0);
            // Se selecciona el tercer registro
            PaginaDetallesRegistro paginaDetalles = paginaRegistros.SeleccionarResultado(1);

            // Acción _______________________________________________________________________
            // Se cambia la calificacion
            paginaDetalles.clickEstrella(4);

            // Verificación _________________________________________________________________
            string resultado = paginaDetalles.ObtenerCantidadCalificaciones();
            Assert.That(resultado, Is.EqualTo("(1)"));
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 3
        [Test]
        public void PaginaDetallesRegistro_RevisarAlCalificarActualizarMensaje_DeberiaSerDos()
        {
            // Preparación __________________________________________________________________
            PaginaHome paginaHome = IniciarSesion();
            // Se realiza una búsqueda con "Iphone"
            PaginaBusqueda paginaBusqueda = paginaHome.Buscar("Iphone");
            // Se selecciona el primer resultado
            PaginaVerRegistros paginaRegistros = paginaBusqueda.SeleccionarResultado(0);
            // Se selecciona el cuarto registro
            PaginaDetallesRegistro paginaDetalles = paginaRegistros.SeleccionarResultado(3);

            // Acción _______________________________________________________________________
            // Se cambia la calificacion
            paginaDetalles.clickEstrella(2);

            // Verificación _________________________________________________________________
            string resultado = paginaDetalles.ObtenerMensajeCalificacion();
            Assert.That(resultado, Is.EqualTo("Ha calificado con: 2"));
        }

        private PaginaHome IniciarSesion()
        {
            PaginaHome paginaHome = new PaginaHome(this.driver);
            driver.Navigate().GoToUrl(paginaHome.ObtenerURL());
            PaginaIngresar paginaIngresar = paginaHome.IrAIngresarUsuario();
            paginaHome = paginaIngresar.IniciarSesion("Usuario1*", "Usuario1*");
            return paginaHome;
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
