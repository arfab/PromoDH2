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


        [Required(ErrorMessage = "Ingrese la marca")]
        public int marca_id { get; set; }

        [Required(ErrorMessage = "Ingrese el día")]
        public int dia { get; set; }

        [Required(ErrorMessage = "Ingrese el mes")]
        public int mes { get; set; }

        [Required(ErrorMessage = "Ingrese el año")]
        public int anio { get; set; }

        [Required(ErrorMessage = "Ingrese el CP")]
        public string CP { get; set; }

        [Required(ErrorMessage = "Ingrese la fecha de nacimiento")]
        [DataType(DataType.Date)]
        public DateTime fecha_nacimiento { get; set; }

        [Required(ErrorMessage = "Ingrese si desea recibir información")]
        public bool recibir_info { get; set; }

        public int premio_id_ret;
        public int user_id_ret;
        public int premio_rango_id_ret;



    }
}
