using System;
using System.ComponentModel.DataAnnotations;

namespace LoCoMPro.Utils.Validadores
{
    // Atributo validador para asegurarse de que dos propiedades sean iguales
    public class DebeCoincidir : ValidationAttribute
    {
        // Segunda popiedad con la que comparar
        private readonly string otraPropiedad;

        // Constructor
        public DebeCoincidir(string otraPropiedad)
        {
            this.otraPropiedad = otraPropiedad;
        }

        // Sobreescribit IsValid para que funncione correctamente
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            // Obtiene el valor de la otra propiedad
            var valorOtraPropiedad = validationContext.ObjectInstance.GetType()
                .GetProperty(otraPropiedad)
                ?.GetValue(validationContext.ObjectInstance, null);

            // Si los valores son distintos
            if (!Equals(value, valorOtraPropiedad))
            {
                // Devuelve el error, si lo hay, sino, devuelve un mensaje de error por defecto
                return new ValidationResult(ErrorMessage ?? "Las dos propiedades deben coincidir.");
            }
            // Si son iguales
            else
            {
                // Devuelve exito
                return ValidationResult.Success!;
            }
        }
    }
}
