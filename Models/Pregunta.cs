using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using Dapper;

namespace PromoDH.Models
{
    public class PreguntaPromo
    {

        public string errorDesc = "";

        public int id { get; set; }
       
        public string pregunta { get; set; }

        public string r1 { get; set; }

        public string r2 { get; set; }

        public string r3 { get; set; }

        public int rc { get; set; }

        public int rsel { get; set; }

    }
}
