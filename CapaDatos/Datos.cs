using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace PromoDH.CapaDatos
{
    public class Datos  
    {

        static readonly string strConnectionString = Tools.GetConnectionString();


        public static List<Models.Premio> ObtenerGanadores()
        {
            List<Models.Premio> lPremios = new List<Models.Premio>();

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                lPremios = con.Query<Models.Premio>("spObtenerGanadores").ToList();
            }

            return lPremios;
        }

        public static List<Models.Sembrado> ObtenerSembrado()
        {
            List<Models.Sembrado> lSembrado = new List<Models.Sembrado>();

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                lSembrado = con.Query<Models.Sembrado>("spObtenerPremios").ToList();
            }

            return lSembrado;
        }

    }
}
