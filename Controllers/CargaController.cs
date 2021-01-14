using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using PromoDH.CapaDatos;
using PromoDH.Models;
using Microsoft.Extensions.Configuration;
using reCAPTCHA.AspNetCore.Attributes;
using Newtonsoft.Json;
using System.Text;
using System.IO;

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

            HttpContext.Session.SetString("CODIGO", "");
            HttpContext.Session.SetString("DNI", "");
            HttpContext.Session.SetString("PREMIO_ID", "");
            HttpContext.Session.SetString("REGISTRO_ID", "");
            HttpContext.Session.SetString("PREMIO_RANGO_ID","");
            HttpContext.Session.Remove("CODIGO");
            HttpContext.Session.Remove("DNI");
            HttpContext.Session.Remove("PREMIO_ID");
            HttpContext.Session.Remove("REGISTRO_ID");
            HttpContext.Session.Remove("PREMIO_RANGO_ID");



            ViewBag.ListOfMarcas = Datos.ObtenerMarcas();
            ViewBag.ListOfProvincias = Datos.ObtenerProvincias();


            return View();
        }

        [HttpPost]
        [ValidateRecaptcha]
        public IActionResult CargarCodigo([Bind] Registro registro)
        {
            string sRet;

            try

            {
                sRet = Validar(registro);



                if (ModelState.IsValid)
                {

                // Por ahora hardcodeo la marca hasta que esté en el frontend
                //registro.marca_id = 1;

      
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
                    string sCodigo = string.Concat(registro.dia.ToString().PadLeft(2, '0'), registro.mes.ToString().PadLeft(2, '0'), registro.anio.ToString());

                    HttpContext.Session.SetString("CODIGO", sCodigo);
                    HttpContext.Session.SetString("DNI", registro.Dni);


                    HttpContext.Session.SetString("PREMIO_ID", registro.premio_id_ret.ToString());
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
                    ViewBag.Message = "Algunos campos estan incompletos.";
                   
                }

              
            }

            //ME FIJO SI EL PROBLEMA ES EL CAPTCHA
            var state = ViewData.ModelState.FirstOrDefault(x => x.Key.Equals("Recaptcha"));
            if (state.Value != null && state.Value.Errors.Any(x => !string.IsNullOrEmpty(x.ErrorMessage)))
            {
                ViewBag.Message = "Revise el CAPTCHA.";
                
            }
            else
            {
                ViewBag.Message = "Faltan datos obligatorios";
                
            }


            }
            catch (Exception)
            {
                ViewBag.Message = "Ha ocurrido un error en el sitio. Inténtelo nuevamente.";
                // return RedirectToAction("Index", "Home");
            }

            ViewBag.ListOfMarcas = Datos.ObtenerMarcas();
            ViewBag.ListOfProvincias = Datos.ObtenerProvincias();

            return View(registro);
        }

        public string Validar(Registro registro)
        {
            string sRet="";

           // object m = null;
           // string s = m.ToString();

            if (registro.Nombre.Trim().Length > 50) return "El Nombre no debe exceder los 50 caracteres";
            if (registro.Nombre.Trim().Length == 0) return "Algunos campos están incompletos.";

            if (registro.Apellido.Trim().Length > 50) return "El Apellido no debe exceder los 50 caracteres";
            if (registro.Apellido.Trim().Length == 0) return "Algunos campos están incompletos.";

            if (registro.Dni.Trim().Length == 0) return "Algunos campos están incompletos.";
            
            if (registro.Dni.Trim() == "") return "Algunos campos están incompletos.";
            if (!int.TryParse(registro.Dni, out _)) return "El DNI no es válido";
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
            catch (Exception)
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