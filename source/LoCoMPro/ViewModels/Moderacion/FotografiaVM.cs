using System.ComponentModel.DataAnnotations;

namespace LoCoMPro.ViewModels.Moderacion
{
    public class FotografiaVM
    {
        // Bytes de la fotografía
        [Display(Name = "Fotografía")]
        public required byte[] foto { get; set; }
    }
}
