using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using PromoDH.CapaDatos;
using PromoDH.Models;

namespace PromoDH.Controllers
{
    public class CargaController : Controller
    {
        public IActionResult Index()
        {
        
            return View();
        }

        public IActionResult NoGano()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CargarCodigo()
        {

            ViewBag.ListOfMarcas = Datos.ObtenerMarcas();
            ViewBag.ListOfProvincias = Datos.ObtenerProvincias();


            return View();
        }

        [HttpPost]
        public IActionResult CargarCodigo([Bind] Registro registro)
        {
            string sRet;

         

            if (ModelState.IsValid)
            {

                // Por ahora hardcodeo la marca hasta que esté en el frontend
                //registro.marca_id = 1;

                sRet = Validar(registro);

                if (sRet != "")
                {
                    ViewBag.Message = sRet;

                    ViewBag.ListOfMarcas = Datos.ObtenerMarcas();
                    ViewBag.ListOfProvincias = Datos.ObtenerProvincias();

                    return View(registro);
                }

                if (Datos.InsertarRegistro(registro) > 0)
                {
                    // Guardar datos registro y premio en sesión
                    HttpContext.Session.SetString("PREMIO_ID", registro.premio_id_ret.ToString());
                    HttpContext.Session.SetString("REGISTRO_ID", registro.user_id_ret.ToString());
                    HttpContext.Session.SetString("PREMIO_RANGO_ID", registro.premio_rango_id_ret.ToString());

                    if (registro.premio_id_ret > 0)
                        return RedirectToAction("PreguntaAzar", "Pregunta");
                    else
                        return RedirectToAction("NoGano", "Carga");

                    //return Redirect("~/");
                }
                else

                {
                    ViewBag.Message = "Algunos campos están incompletos.";
                }
            }

            ViewBag.ListOfMarcas = Datos.ObtenerMarcas();
            ViewBag.ListOfProvincias = Datos.ObtenerProvincias();

            return View(registro);
        }

        public string Validar(Registro registro)
        {
            string sRet="";

            if (registro.Nombre.Trim().Length > 50) return "El Nombre no debe exceder los 50 caracteres";
            if (registro.Nombre.Trim().Length == 0) return "Algunos campos están incompletos.";

            if (registro.Apellido.Trim().Length > 50) return "El Apellido no debe exceder los 50 caracteres";
            if (registro.Apellido.Trim().Length == 0) return "Algunos campos están incompletos.";

            if (registro.Dni.Trim().Length == 0) return "Algunos campos están incompletos.";
            /* Falta condicion para que sea numerico*/
            if (registro.Dni.Trim() == "") return "Algunos campos están incompletos.";
            if (registro.Dni.Length < 7) return "El DNI no es válido";
            if (registro.Dni.Length > 8) return "El DNI no es válido";
            if (registro.Dni.StartsWith("00")) return "El DNI no es válido";
            if (registro.Dni.StartsWith("111111")) return "El DNI no es válido";
            if (registro.Dni.StartsWith("222222")) return "El DNI no es válido";
            if (registro.Dni.StartsWith("333333")) return "El DNI no es válido";
            if (registro.Dni.StartsWith("444444")) return "El DNI no es válido";
            if (registro.Dni.StartsWith("555555")) return "El DNI no es válido";
            if (registro.Dni.StartsWith("666666")) return "El DNI no es válido";
            if (registro.Dni.StartsWith("777777")) return "El DNI no es válido";
            if (registro.Dni.StartsWith("888888")) return "El DNI no es válido";
            if (registro.Dni.StartsWith("999999")) return "El DNI no es válido";
            if (registro.Dni.StartsWith("123456")) return "El DNI no es válido";
            if (registro.Dni.StartsWith("987654")) return "El DNI no es válido";

            if (registro.dia.ToString().Trim().Length == 0) return "Algunos campos están incompletos.";
            if (registro.mes.ToString().Trim().Length == 0) return "Algunos campos están incompletos.";
            if (registro.dia.ToString().Trim().Length == 0) return "Algunos campos están incompletos.";

            if (registro.fecha_nacimiento.ToString().Trim().Length == 0) return "Algunos campos están incompletos.";

            try
            {
                DateTime d = new DateTime(registro.anio, registro.mes, registro.dia, 0, 0, 0, 0);

                if (registro.dia < 1 || registro.dia > 31) return "Fecha Inválida";
                if (registro.mes < 1 || registro.dia > 12) return "Fecha Inválida";
                if (registro.anio < 21 || registro.anio > 24) return "Fecha Inválida";
            }
            catch (Exception e)
            {

                string sCodigo;

                sCodigo = string.Concat(registro.dia.ToString().PadLeft(2, '0'), registro.mes.ToString().PadLeft(2, '0'), registro.anio.ToString());


                if (Datos.EsCodigoValido(sCodigo)==0) 
                    return "Fecha Inválida";
                else
                    return "";
         
            }

            if (registro.bases != true) return "Debe aceptar las bases y condiciones";




            return sRet;

        }


    }
}