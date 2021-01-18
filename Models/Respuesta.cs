using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromoDH.Models
{
    public class Respuesta
    {

        public string errorDesc = "";

        public int id { get; set; }

        public int registro_id { get; set; }

        public string pregunta_id { get; set; }

        public string respuesta_nro { get; set; }

        public string correcta_nro { get; set; }

        public string respuesta { get; set; }

        public DateTime fecha_hora_respuesta { get; set; }

        public int id_sembrado { get; set; }

        public DateTime fecha_hora_sembrado { get; set; }

        public string Resultado { get; set; }

    }
}
