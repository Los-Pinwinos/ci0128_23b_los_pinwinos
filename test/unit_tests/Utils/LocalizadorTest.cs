using LoCoMPro.Utils;


namespace LoCoMProTests.Utils
{
    // Hecho por: Enrique Guillermo Vílchez Lizano - C18477 - Sprint 3
    [TestClass]
    public class LocalizadorTest
    {
        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477 - Sprint 3
        [TestMethod]
        public void distanciaKm_ValidacionDosCoordenadasIguales_DeberiaDevolverCeroKm()
        {
            // Preparación
            double latOrigen = 1, latDestino = 1;
            double lonOrigen = 2, lonDestino = 2;

            // Acción
            double kilometros = Localizador.DistanciaKm(latOrigen, lonOrigen, latDestino, lonDestino);

            // Verificación
            Assert.AreEqual(0, kilometros);
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477 - Sprint 3
        [TestMethod]
        public void distanciaKm_ValidacionDosCoordenadasCercanas_DeberiaDevolverEntreCincoYQuinceKm()
        {
            // Preparación
            // Coordenadas entre las ciudades de San José y Heredia (relativamente cercanas).
            double latOrigen = 9.9281, latDestino = 9.9986;
            double lonOrigen = -84.0907, lonDestino = -84.1171;

            // Acción
            double kilometros = Localizador.DistanciaKm(latOrigen, lonOrigen, latDestino, lonDestino);

            // Verificación
            Assert.IsTrue(5 <= kilometros && kilometros <= 15);
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477 - Sprint 3
        [TestMethod]
        public void distanciaKm_ValidacionDosCoordenadasMedianamenteLejanas_DeberiaDevolverEntreDiezYVeinteKm()
        {
            // Preparación
            // Coordenadas entre las ciudades de San José y Cartago (relativamente lejanas).
            double latOrigen = 9.9281, latDestino = 9.8622;
            double lonOrigen = -84.0907, lonDestino = -83.9211;

            // Acción
            double kilometros = Localizador.DistanciaKm(latOrigen, lonOrigen, latDestino, lonDestino);

            // Verificación
            Assert.IsTrue(10 <= kilometros && kilometros <= 20);
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477 - Sprint 3
        [TestMethod]
        public void distanciaKm_ValidacionDosCoordenadasLejanas_DeberiaDevolverEntreCienYCientoVeinteKm()
        {
            // Preparación
            // Coordenadas entre las ciudades de San José y Limón (lejanas).
            double latOrigen = 9.9281, latDestino = 9.9929;
            double lonOrigen = -84.0907, lonDestino = -83.0377;

            // Acción
            double kilometros = Localizador.DistanciaKm(latOrigen, lonOrigen, latDestino, lonDestino);

            // Verificación
            Assert.IsTrue(100 <= kilometros && kilometros <= 120);
        }
    }
}
