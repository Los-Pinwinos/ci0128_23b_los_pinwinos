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
            textoEquipo = "LoCoMPro fue realizado para el curso Proyecto Integrador de Ingenier�a de Software y Bases de Datos de la Universidad de Costa Rica. El equipo est� compuesto por 5 estudiantes de Computaci�n e Inform�tica que dedicaron su pasi�n en este trabajo. Esperamos que lo disfruten.";
            textoAngie = "Hola, soy Angie. Me gusta mucho dibujar, dise�ar, cocinar y el cine. Me encanta la programaci�n porque para m� es una forma de resolver cualquier problema.";
            textoEmilia = "Hola, soy Emilia. Me gusta mucho nadar, bailar y ver series en Netflix. Comenc� a estudiar computaci�n en el a�o 2021. Me gusta mucho aprender cosas nuevas�y�programar.";
            textoEnrique = "Hola, soy Enrique. Disfruto mucho ir al gimnasio y la actividad f�sica. Tambi�n me gusta tocar guitarra ac�stica y el�ctrica. Me encantan los temas de I.A�y�matem�tica.";
            textoLuis = "Hola, soy Luis. Me gusta tocar m�sica, programar y jugar�videojuegos.";
            textoKenneth = "Hola, soy Kenneth, pero algunos me llaman: El Diesel Villalobos, entre mis m�sculos corre gasolina y entre mis venas sangre caliente. \"Quisiera ser una mosca, para pararme en tu piel\" JV.";
        }
        public void OnGet()
        {
        }
    }
}
