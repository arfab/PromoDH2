using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using PromoDH.Models;

namespace PromoDH.CapaDatos
{
    public class Datos  
    {

        static readonly string strConnectionString = Tools.GetConnectionString();


        public static int InsertarRegistro(Registro registro)
        {
            int rowAffected = 0;

            try
            {


                using (IDbConnection con = new SqlConnection(strConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@nombre", registro.Nombre);
                    parameters.Add("@apellido", registro.Apellido);
                    parameters.Add("@dni", registro.Dni);
                    parameters.Add("@marca_id", registro.marca_id);
                    parameters.Add("@fecha_nacimiento", registro.fecha_nacimiento);
                    parameters.Add("@codigo", registro.Codigo);
                    parameters.Add("@provincia", registro.Provincia);
                    parameters.Add("@localidad", registro.Localidad);
                    parameters.Add("@direccion", registro.Direccion);
                    parameters.Add("@telefono", registro.Telefono);
                    parameters.Add("@email", registro.Email);
                    parameters.Add("@CP", "");
                    parameters.Add("@recibir_info", registro.recibir_info);
                    parameters.Add("@activo", 1);
                    parameters.Add("@premio_id_ret", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    parameters.Add("@user_id_ret", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    parameters.Add("@premio_rango_id_ret", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    rowAffected = con.Execute("spInsertarRegistro", parameters, commandType: CommandType.StoredProcedure);

                    registro.premio_id_ret = parameters.Get<int>("premio_id_ret");
                    registro.user_id_ret = parameters.Get<int>("user_id_ret");
                    registro.premio_rango_id_ret = parameters.Get<int>("premio_rango_id_ret");


                }

            }
            catch (Exception ex)
            {
                registro.errorDesc = ex.Message;
            }

            return rowAffected;
        }

        public static int InsertarCodigo(Registro registro)
        {
            int rowAffected = 0;

            try
            {


                using (IDbConnection con = new SqlConnection(strConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@nombre", registro.Nombre);
                    parameters.Add("@apellido", registro.Apellido);
                    parameters.Add("@dni", registro.Dni);
                    parameters.Add("@fecha_nacimiento", registro.fecha_nacimiento);
                    parameters.Add("@codigo", registro.Codigo);
                    parameters.Add("@provincia", registro.Provincia);
                    parameters.Add("@recibir_info", registro.recibir_info);
                    parameters.Add("@activo", 1);
                    parameters.Add("@premio_id_ret", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    parameters.Add("@user_id_ret", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    parameters.Add("@premio_rango_id_ret", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    rowAffected = con.Execute("spInsertarCodigo", parameters, commandType: CommandType.StoredProcedure);

                    registro.premio_id_ret = parameters.Get<int>("premio_id_ret");
                    registro.user_id_ret = parameters.Get<int>("user_id_ret");
                    registro.premio_rango_id_ret = parameters.Get<int>("premio_rango_id_ret");


                }

            }
            catch (Exception ex)
            {
                registro.errorDesc = ex.Message;
            }

            return rowAffected;
        }




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




        public static PreguntaPromo ObtenerPreguntaAzar()
        {
            PreguntaPromo preg = new PreguntaPromo();

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                //DynamicParameters parameter = new DynamicParameters();
                preg = con.Query<PreguntaPromo>("spObtenerPreguntaAzar", commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            return preg;
        }

        public static int InsertarRespuesta(PreguntaPromo preg, int iRegistro, int iPremioRango)
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
                    parameters.Add("@registro_id", iRegistro);
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
