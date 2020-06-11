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

        static string strConnectionString = "Data Source=190.210.162.183;Initial Catalog=PromoTopline2020;Persist Security Info=True;User ID=PromoTopline2020;Password=PromoTopline2020";

        public string errorDesc = "";

        public int id { get; set; }
       
        public string pregunta { get; set; }

        public string r1 { get; set; }

        public string r2 { get; set; }

        public string r3 { get; set; }

        public int rc { get; set; }

        public static PreguntaPromo ObtenerPreguntaAzar()
        {
            PreguntaPromo preg = new PreguntaPromo();

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                //DynamicParameters parameter = new DynamicParameters();
                preg = con.Query<PreguntaPromo>("spObtenerPreguntaAzar",  commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            return preg;
        }
    }
}
