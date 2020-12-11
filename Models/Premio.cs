using System.ComponentModel.DataAnnotations;

namespace PromoDH.Models
{
    public class Premio
    {

        public int Id { get; set; }

        public string Dni { get; set; }

        public string nombre { get; set; }

        public string apellido { get; set; }

        public string PremioDesc { get; set; }

        public string Codigo { get; set; }

        public string Fecha_str { get; set; }



    }
}
