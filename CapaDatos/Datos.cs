﻿using System.Collections.Generic;
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

                    string sCodigo;

                    sCodigo = string.Concat(registro.dia.ToString().PadLeft(2,'0'),registro.mes.ToString().PadLeft(2,'0'),registro.anio.ToString());

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@nombre", registro.Nombre);
                    parameters.Add("@apellido", registro.Apellido);
                    parameters.Add("@dni", registro.Dni);
                    parameters.Add("@marca_id", registro.marca_id);
                    parameters.Add("@fecha_nacimiento", registro.fecha_nacimiento);
                    parameters.Add("@codigo", sCodigo);
                    parameters.Add("@provincia", registro.Provincia);
                    parameters.Add("@localidad", registro.Localidad);
                    parameters.Add("@direccion", registro.Direccion);
                    parameters.Add("@telefono", registro.Telefono);
                    parameters.Add("@email", registro.Email);
                    parameters.Add("@CP", registro.CP);
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


        public static int ModificarRegistro(Ganador ganador, int iRegistro)
        {

            try
            {


                using (IDbConnection con = new SqlConnection(strConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();


                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@id", iRegistro);
                    parameters.Add("@localidad", ganador.Localidad);
                    parameters.Add("@direccion", ganador.Direccion);
                    parameters.Add("@telefono", ganador.Telefono);
                  
                    con.Execute("spModificarRegistro", parameters, commandType: CommandType.StoredProcedure);




                }

            }
            catch (Exception ex)
            {
                return -1;
            }

            return 0;
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

                    string sCodigo;

                    sCodigo = string.Concat(registro.dia.ToString().PadLeft(2, '0'), registro.mes.ToString().PadLeft(2, '0'), registro.anio.ToString());


                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@nombre", registro.Nombre);
                    parameters.Add("@apellido", registro.Apellido);
                    parameters.Add("@dni", registro.Dni);
                    parameters.Add("@fecha_nacimiento", registro.fecha_nacimiento);
                    parameters.Add("@codigo", sCodigo);
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


        public static List<Models.PreguntaPromo> ObtenerPreguntas()
        {
            List<Models.PreguntaPromo> lPregunta = new List<Models.PreguntaPromo>();

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                lPregunta = con.Query<Models.PreguntaPromo>("spObtenerPreguntas").ToList();
            }

            return lPregunta;
        }

        public static List<Models.Respuesta> ObtenerRespuestas()
        {
            List<Models.Respuesta> lRespuesta = new List<Models.Respuesta>();

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                lRespuesta = con.Query<Models.Respuesta>("spObtenerRespuestas").ToList();
            }

            return lRespuesta;
        }

        public static List<Models.Respuesta> ObtenerRespuestasPag(int? pag, int? cantxpag)
        {
            List<Models.Respuesta> lRespuesta = new List<Models.Respuesta>();

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();


                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@pag", pag);
                parameter.Add("@cantxpag", cantxpag);

                lRespuesta = con.Query<Models.Respuesta>("spObtenerRespuestasPag", parameter, commandType: CommandType.StoredProcedure).ToList();
            }

            return lRespuesta;
        }


        public static int ObtenerRespuestasCantidad()
        {

            var lRes = 0;


            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                lRes = con.Query<int>("spObtenerRespuestasCantidad", commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            return lRes;
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

        public static List<Models.Contacto> ObtenerConsultas(string apellido, string email)
        {
            List<Models.Contacto> lContacto = new List<Models.Contacto>();

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@apellido", apellido);
                parameter.Add("@email", email);


                lContacto = con.Query<Models.Contacto>("spObtenerConsultas", parameter, commandType: CommandType.StoredProcedure).ToList();
            }

            return lContacto;
        }

        public static List<Models.Contacto> ObtenerConsultasPag(string apellido, string email, int? pag, int? cantxpag)
        {
            List<Models.Contacto> lContacto = new List<Models.Contacto>();

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@apellido", apellido);
                parameter.Add("@email", email);
                parameter.Add("@pag", pag);
                parameter.Add("@cantxpag", cantxpag);

                lContacto = con.Query<Models.Contacto>("spObtenerConsultasPag", parameter, commandType: CommandType.StoredProcedure).ToList();
            }

            return lContacto;
        }

        public static int ObtenerConsultasCantidad(string apellido, string email)
        {

            var lRes = 0;


            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@apellido", apellido);
                parameter.Add("@email", email);

                lRes = con.Query<int>("spObtenerConsultasCantidad", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            return lRes;
        }

        public static List<Models.Contacto> ObtenerConsulta(int id)
        {
            List<Models.Contacto> lContacto = new List<Models.Contacto>();

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@id", id);

                lContacto = con.Query<Models.Contacto>("spObtenerConsulta", parameter, commandType: CommandType.StoredProcedure).ToList();
            }

            return lContacto;
        }


        public static List<Models.Registro> ObtenerRegistros(string dni, int activo)
        {
            List<Models.Registro> lRegistro = new List<Models.Registro>();

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@dni", dni);
                parameter.Add("@activo", activo);


                lRegistro = con.Query<Models.Registro>("spObtenerRegistros", parameter, commandType: CommandType.StoredProcedure).ToList();
            }

            return lRegistro;
        }


        public static List<Models.Registro> ObtenerRegistrosPag(string dni, int activo, int? pag, int? cantxpag)
        {
            List<Models.Registro> lRegistro = new List<Models.Registro>();

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@dni", dni);
                parameter.Add("@activo", activo);
                parameter.Add("@pag", pag);
                parameter.Add("@cantxpag", cantxpag);


                lRegistro = con.Query<Models.Registro>("spObtenerRegistrosPag", parameter, commandType: CommandType.StoredProcedure).ToList();
            }

            return lRegistro;
        }

        public static int ObtenerRegistrosCantidad()
        {

            var lRes = 0;

            //List<Torneo> lRes = new List<Torneo>();

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                lRes = con.Query<int>("spObtenerRegistrosCantidad",  commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            return lRes;
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


        public static List<Models.Provincia> ObtenerProvincias()
        {
            List<Models.Provincia> lProvincias = new List<Models.Provincia>();

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                lProvincias = con.Query<Models.Provincia>("spObtenerProvincias").ToList();
            }

            return lProvincias;
        }

        public static List<Models.Marca> ObtenerMarcas()
        {
            List<Models.Marca> lMarcas = new List<Models.Marca>();

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                lMarcas = con.Query<Models.Marca>("spObtenerMarcas").ToList();
            }

            return lMarcas;
        }

        public static int EsCodigoValido(string codigo)
        {

            var lRes = 0;


            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@codigo", codigo);


                lRes = con.Query<int>("spEsCodigoValido", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            return lRes;
        }




        public static int InsertarConsulta(Contacto contacto)
        {
            int rowAffected = 0;

            try
            {


                using (IDbConnection con = new SqlConnection(strConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@nombre", contacto.Nombre);
                    parameters.Add("@apellido", contacto.Apellido);
                    parameters.Add("@dni", contacto.Dni);
                    parameters.Add("@pais", "");
                    parameters.Add("@provincia", contacto.Provincia);
                    parameters.Add("@localidad", contacto.Localidad);
                    parameters.Add("@direccion", contacto.Direccion);
                    parameters.Add("@telefono", string.Concat(contacto.Area,"-",contacto.Telefono));
                    parameters.Add("@movil", string.Concat(contacto.Area, "-",contacto.Movil));
                    parameters.Add("@email", contacto.Email);
                    parameters.Add("@consulta", contacto.Consulta);

                    rowAffected = con.Execute("spInsertarConsulta", parameters, commandType: CommandType.StoredProcedure);

               

                }

            }
            catch (Exception ex)
            {
                contacto.errorDesc = ex.Message;
            }

            return rowAffected;
        }



        public static Models.Usuario Login(string usuario, string clave)
        {
            Models.Usuario usu = new Models.Usuario();

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@usuario", usuario);
                parameter.Add("@password", clave);
                usu = con.Query<Models.Usuario>("spLoginAdmin", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();

            }

            return usu;
        }


        public static int SetearIngresoEscribanoAdmin(int iId, out string sError)
        {
            int rowAffected = 0;
            sError = "";

            try
            {


                using (IDbConnection con = new SqlConnection(strConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@id", iId);
                    rowAffected = con.Execute("spModificarEscribanoAdmin", parameters, commandType: CommandType.StoredProcedure);

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
