using System.ComponentModel.DataAnnotations;


namespace PromoDH.Models
{

    public class Provincia
    {
        public int id { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string descripcion { get; set; }
    }
}
