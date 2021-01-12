
using System.ComponentModel.DataAnnotations;

namespace PromoDH.Models
{
    public class Contacto
    {

        public string errorDesc = "";

        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese el Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese el Apellido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Ingrese la Provincia")]
        public string Provincia { get; set; }

        [Required(ErrorMessage = "Ingrese la Direccion")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Ingrese la Ciudad")]
        public string Localidad { get; set; }

        [Required(ErrorMessage = "Ingrese el Area")]
        public string Area { get; set; }

        [Required(ErrorMessage = "Ingrese el Teléfono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Ingrese el Area del Móvil")]
        public string AreaMovil { get; set; }

        [Required(ErrorMessage = "Ingrese el Móvil")]
        public string Movil { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Ingrese el Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ingrese el Dni")]
        public string Dni { get; set; }

        [Required(ErrorMessage = "Ingrese el comentario")]
        public string Consulta { get; set; }

    }
}
