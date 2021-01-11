using System.ComponentModel.DataAnnotations;


namespace PromoDH.Models
{
    public class Marca
    {
        public int id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string nombre { get; set; }


    }
}
