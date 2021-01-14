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


        //private const string secret_key = "6LfgtgwUAAAAAMu6LktSBMJ-WjNRl_oPmnHNo9L3";

        //public static bool GoogleValidate(HttpRequest Request, string hostname)
        //{
        //    var g_captcha_response = Request.Form["g-recaptcha-response"];
        //    if (!string.IsNullOrEmpty(g_captcha_response))
        //    {
        //        var response = ExecuteVerification(g_captcha_response);
        //        if (!response.StartsWith("ERROR:"))
        //        {
        //            var json_obj = JsonConvert.DeserializeObject<ValidateResponse>(response);
        //            if (json_obj.success)
        //            {
        //                if (json_obj.hostname.ToString().ToLower() == hostname.ToString().ToLower())
        //                    return true;
        //            }
        //        }
        //    }
        //    return false;
        //}


        //public class ValidateResponse
        //{
        //    public bool success { get; set; }
        //    public DateTime challenge_ts { get; set; }
        //    public string hostname { get; set; }
        //    [JsonProperty("error-codes")]
        //    public List<string> error_codes { get; set; }
        //}


        //private static string ExecuteVerification(string g_captcha_response)
        //{
        //    System.Net.WebRequest request = System.Net.WebRequest.Create("https://www.google.com/recaptcha/api/siteverify?secret=" + secret_key + "&response=" + g_captcha_response + "&remoteip=" + GetIPv4Address());
        //    request.Timeout = 5 * 1000; // 5 Seconds to avoid getting locked up
        //    request.Method = "POST";
        //    request.ContentType = "application/json";
        //    try
        //    {
        //        byte[] byteArray = Encoding.UTF8.GetBytes("");
        //        request.ContentLength = byteArray.Length;
        //        Stream dataStream = request.GetRequestStream();
        //        dataStream.Write(byteArray, 0, byteArray.Length);
        //        dataStream.Close();
        //        System.Net.WebResponse response = request.GetResponse();
        //        dataStream = response.GetResponseStream();
        //        StreamReader reader = new StreamReader(dataStream);
        //        string responseFromServer = reader.ReadToEnd();
        //        reader.Close();
        //        response.Close();
        //        return responseFromServer;
        //    }
        //    catch (Exception ex)
        //    {
        //        return "ERROR: " + ex.Message;
        //    }
        //}


        //public static string GetIPv4Address()
        //{
        //    string GetIPv4AddressRet = string.Empty;
        //    string strHostName = System.Net.Dns.GetHostName();
        //    System.Net.IPHostEntry iphe = System.Net.Dns.GetHostEntry(strHostName);

        //    foreach (System.Net.IPAddress ipheal in iphe.AddressList)
        //    {
        //        if (ipheal.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
        //            GetIPv4AddressRet = ipheal.ToString();
        //    }
        //    return GetIPv4AddressRet;
        //}



        //public static bool ReCaptchaPassed(string gRecaptchaResponse)
        //{
        //    HttpClient httpClient = new HttpClient();

        //    var res = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret=6LfgtgwUAAAAAMu6LktSBMJ-WjNRl_oPmnHNo9L3&response={gRecaptchaResponse}").Result;

        //    if (res.StatusCode != HttpStatusCode.OK)
        //        return false;

        //    string JSONres = res.Content.ReadAsStringAsync().Result;
        //    dynamic JSONdata = JObject.Parse(JSONres);

        //    if (JSONdata.success != "true")
        //        return false;

        //    return true;
        //}

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


            if (ModelState.IsValid)
            {
               
                
                var state = ViewData.ModelState.FirstOrDefault(x => x.Key.Equals("Recaptcha"));
                if (state.Value != null && state.Value.Errors.Any(x => !string.IsNullOrEmpty(x.ErrorMessage)))
                {
                    ViewBag.Message = "Falló el CAPTCHA.";
                    ViewBag.ListOfMarcas = Datos.ObtenerMarcas();
                    ViewBag.ListOfProvincias = Datos.ObtenerProvincias();
                    return View(registro);
                    //return Page();}
                }

                // Por ahora hardcodeo la marca hasta que esté en el frontend
                //registro.marca_id = 1;

                //if (!ReCaptchaPassed(Request.Form["g-recaptcha-response"]))
                //{
                //    //ModelState.AddModelError(string.Empty, "You failed the CAPTCHA.");
                //    ViewBag.Message = "Falló el CAPTCHA.";
                //    ViewBag.ListOfMarcas = Datos.ObtenerMarcas();
                //    ViewBag.ListOfProvincias = Datos.ObtenerProvincias();
                //    return View(registro);
                //    //return Page();
                //}


                //if (!GoogleValidate(Request, Request.Host.ToString()))
                //{
                //    ViewBag.Message = "Falló el CAPTCHA.";
                //    ViewBag.ListOfMarcas = Datos.ObtenerMarcas();
                //    ViewBag.ListOfProvincias = Datos.ObtenerProvincias();
                //    return View(registro);
                //    //return Page();
                //}



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

            ViewBag.ListOfMarcas = Datos.ObtenerMarcas();
            ViewBag.ListOfProvincias = Datos.ObtenerProvincias();

            return View(registro);
        }

        public string Validar(Registro registro)
        {
            string sRet="";

            if (registro.Nombre.Trim().Length > 50) return "El Nombre no debe exceder los 50 caracteres";
            if (registro.Nombre.Trim().Length == 0) return "Algunos campos estan incompletos.";

            if (registro.Apellido.Trim().Length > 50) return "El Apellido no debe exceder los 50 caracteres";
            if (registro.Apellido.Trim().Length == 0) return "Algunos campos estan incompletos.";

            if (registro.Dni.Trim().Length == 0) return "Algunos campos estan incompletos.";
            
            if (registro.Dni.Trim() == "") return "Algunos campos están incompletos.";
            if (!int.TryParse(registro.Dni, out _)) return "El DNI no es valido";
            if (registro.Dni.Length < 7) return "El DNI no es valido";
            if (registro.Dni.Length > 8) return "El DNI no es valido";
            if (registro.Dni.StartsWith("00")) return "El DNI no es válido";
            if (registro.Dni.StartsWith("111111")) return "El DNI no es valido";
            if (registro.Dni.StartsWith("222222")) return "El DNI no es valido";
            if (registro.Dni.StartsWith("333333")) return "El DNI no es valido";
            if (registro.Dni.StartsWith("444444")) return "El DNI no es valido";
            if (registro.Dni.StartsWith("555555")) return "El DNI no es valido";
            if (registro.Dni.StartsWith("666666")) return "El DNI no es valido";
            if (registro.Dni.StartsWith("777777")) return "El DNI no es valido";
            if (registro.Dni.StartsWith("888888")) return "El DNI no es valido";
            if (registro.Dni.StartsWith("999999")) return "El DNI no es valido";
            if (registro.Dni.StartsWith("123456")) return "El DNI no es valido";
            if (registro.Dni.StartsWith("987654")) return "El DNI no es valido";

            if (registro.dia.ToString().Trim().Length == 0) return "Algunos campos estan incompletos.";
            if (registro.mes.ToString().Trim().Length == 0) return "Algunos campos estan incompletos.";
            if (registro.dia.ToString().Trim().Length == 0) return "Algunos campos estan incompletos.";

            if (registro.fecha_nacimiento.ToString().Trim().Length == 0) return "Algunos campos estan incompletos.";

            try
            {
                DateTime d = new DateTime(registro.anio, registro.mes, registro.dia, 0, 0, 0, 0);

                if (registro.dia < 1 || registro.dia > 31) return "Fecha Invalida";
                if (registro.mes < 1 || registro.dia > 12) return "Fecha Invalida";
                if (registro.anio < 21 || registro.anio > 24) return "Fecha Invalida";
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