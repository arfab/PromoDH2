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

        public int rsel { get; set; }


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

        public static int InsertarRespuesta(PreguntaPromo preg, int iRegistro, int iPremioRango )
        {
            int rowAffected = 0;

            try
            {


                using (IDbConnection con = new SqlConnection(strConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@premio_rango_id", iPremioRango);
                    parameters.Add("@registro_id",iRegistro);
                    parameters.Add("@pregunta_id", preg.id);
                    parameters.Add("@respuesta_nro", preg.rsel);
                    parameters.Add("@correcta_nro", preg.rc);

                    rowAffected = con.Execute("spInsertarRespuesta", parameters, commandType: CommandType.StoredProcedure);
                }

            }
            catch (Exception ex)
            {
                preg.errorDesc = ex.Message;
            }

            return rowAffected;
        }

        public static int InsertarPremio(int iRegistro, int iPremioRango, out int iPremio, out string sError)
        {
            int rowAffected = 0;
            iPremio = 0;
            sError = "";

            try
            {


                using (IDbConnection con = new SqlConnection(strConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@registro_id", iRegistro);
                    parameters.Add("@premio_rango_id", iPremioRango);
                    parameters.Add("@premio_id_ret", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    rowAffected = con.Execute("spInsertarPremio", parameters, commandType: CommandType.StoredProcedure);

                   iPremio = parameters.Get<int>("premio_id_ret");

                }

            }
            catch (Exception ex)
            {
                sError = ex.Message;
            }

            return rowAffected;
        }

    }
}
