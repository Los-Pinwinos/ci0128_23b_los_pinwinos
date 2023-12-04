using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoCoMPro.Pages.Home
{
    public class NosotrosModel : PageModel
    {
        public string textoEquipo { get; set; }
        public string textoAngie { get; set; }
        public string textoEmilia { get; set; }
        public string textoLuis { get; set; }
        public string textoKenneth { get; set; }
        public string textoEnrique { get; set; }

        public NosotrosModel() {
            textoEquipo = "LoCoMPro fue realizado para el curso Proyecto Integrador de Ingeniería de Software y Bases de Datos de la Universidad de Costa Rica. El equipo está compuesto por 5 estudiantes de Computación e Informática que dedicaron su pasión en este trabajo. Esperamos que lo disfruten.";
            textoAngie = "Hola, soy Angie. Me gusta mucho dibujar, diseñar, cocinar y el cine. Me encanta la programación porque para mí es una forma de resolver cualquier problema.";
            textoEmilia = "Hola, soy Emilia. Me gusta mucho nadar, bailar y ver series en Netflix. Comencé a estudiar computación en el año 2021. Me gusta mucho aprender cosas nuevas y programar.";
            textoEnrique = "Hola, soy Enrique. Disfruto mucho ir al gimnasio y la actividad física. También me gusta tocar guitarra acústica y eléctrica. Me encantan los temas de I.A y matemática.";
            textoLuis = "Hola, soy Luis. Me gusta tocar música, programar y jugar videojuegos.";
            textoKenneth = "Hola, soy Kenneth, pero algunos me llaman: El Diesel Villalobos, entre mis músculos corre gasolina y entre mis venas sangre caliente. \"Quisiera ser una mosca, para pararme en tu piel\" JV.";
        }
        public void OnGet()
        {
        }
    }
}
