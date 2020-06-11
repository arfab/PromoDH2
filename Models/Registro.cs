using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using Dapper;


namespace PromoDH.Models
{
    public class Registro
    {

        static string strConnectionString = "Data Source=190.210.162.183;Initial Catalog=PromoTopline2020;Persist Security Info=True;User ID=PromoTopline2020;Password=PromoTopline2020";

        public string errorDesc = "";

        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese el Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese el Apellido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Ingrese la Provincia")]
        public string Provincia { get; set; }

        [Required(ErrorMessage = "Ingrese la Direccion")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Ingrese la Ciudad")]
        public string Localidad { get; set; }

        [Required(ErrorMessage = "Ingrese el Teléfono")]
        public string Telefono { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Ingrese el Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ingrese el Dni")]
        public string Dni { get; set; }

        [Required(ErrorMessage = "Ingrese el Código")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Ingrese la fecha de nacimiento")]
        [DataType(DataType.Date)]
        public DateTime fecha_nacimiento { get; set; }

        [Required(ErrorMessage = "Ingrese si desea recibir información")]
        public bool recibir_info { get; set; }

        public int premio_id_ret;
        public int user_id_ret;
        public int premio_rango_id_ret;



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
                    parameters.Add("@premio_id_ret", dbType: DbType.Int32, direction:ParameterDirection.Output);
                    parameters.Add("@user_id_ret", dbType: DbType.Int32,  direction: ParameterDirection.Output);
                    parameters.Add("@premio_rango_id_ret", dbType: DbType.Int32,  direction: ParameterDirection.Output);

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


    }
}
