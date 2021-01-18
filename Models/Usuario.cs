using System;
using System.ComponentModel.DataAnnotations;

namespace PromoDH.Models
{
    public class Usuario
    {

        public int id { get; set; }

        public string usuario { get; set; }

        public string clave { get; set; }

        public string nombre { get; set; }

        public string apellido { get; set; }

        public int escribano { get; set; }

        public DateTime? logged_date { get; set; }


    }
}
