using System;
using System.ComponentModel.DataAnnotations;

namespace PromoDH.Models
{
    public class Ganador
    {

        [Required(ErrorMessage = "Ingrese la Direccion")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Ingrese la Ciudad")]
        public string Localidad { get; set; }

        [Required(ErrorMessage = "Ingrese el Teléfono")]
        public string Telefono { get; set; }

      
    }
}
