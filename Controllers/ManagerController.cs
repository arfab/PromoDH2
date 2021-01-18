using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromoDH.CapaDatos;
using PromoDH.Models;

namespace PromoDH.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("USUARIO_ID").GetValueOrDefault() != 0)
                return View();
            else
                return View("Login");
        }


        [HttpGet]
        public IActionResult Premios()
        {
            if (HttpContext.Session.GetInt32("ESCRIBANO").GetValueOrDefault() > 0 || HttpContext.Session.GetString("USERMANAGER") == "adminmxt") {
                
                return View("Premios", Datos.ObtenerSembrado());
            }
             
            else
            {
                ViewBag.Message = "No está autorizado a ver esta página";
                return View("Index");
            }
              
        }

        [HttpGet]
        public IActionResult Preguntas()
        {
            if (HttpContext.Session.GetInt32("ESCRIBANO").GetValueOrDefault() > 0 || HttpContext.Session.GetString("USERMANAGER") == "adminmxt")
            {

                return View("Preguntas", Datos.ObtenerPreguntas());
            }
            else
            {
                ViewBag.Message = "No está autorizado a ver esta página";
                return View("Index");
            }

        }

        [HttpGet]
        public IActionResult Respuestas()
        {
            if (HttpContext.Session.GetInt32("USUARIO_ID").GetValueOrDefault() != 0)
                return View("Respuestas", Datos.ObtenerRespuestas());
            else
                return View("Login");
        }

        [HttpGet]
        public IActionResult Ganadores()
        {
            if (HttpContext.Session.GetInt32("USUARIO_ID").GetValueOrDefault() != 0)
                return View("Ganadores", Datos.ObtenerGanadores());
            else
                return View("Login");
        }


        [HttpGet]
        public IActionResult Consultas()
        {
            if (HttpContext.Session.GetInt32("USUARIO_ID").GetValueOrDefault() != 0)
                return View("Consultas", Datos.ObtenerConsultas("",""));
            else
                return View("Login");
        }

        [HttpGet]
        public IActionResult Codigos()
        {
            if (HttpContext.Session.GetInt32("USUARIO_ID").GetValueOrDefault() != 0)
                return View("Codigos", Datos.ObtenerRegistros("", -1));
            else
                return View("Login");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login([Bind] Usuario usu)
        {
            string sError="";
            usu = Datos.Login(usu.usuario, usu.clave);

            if (usu != null)
            {
                HttpContext.Session.SetInt32("ESCRIBANO", usu.escribano);
                HttpContext.Session.SetString("USERMANAGER", usu.usuario);

                if (usu.escribano==1)
                {
                    if (usu.logged_date != null)
                    {
                        ViewBag.Message = "El escribano ya ha ingresado anteriormente.";

                        return View();
                    }
                    else
                    {
                        Datos.SetearIngresoEscribanoAdmin(usu.id, out sError);
                        return View("Index");
                    }
                }
                HttpContext.Session.SetInt32("USUARIO_ID", usu.id);
                return View("Index");
            }
            else
            {
                ViewBag.Message = "Usuario o clave incorrectas";

                return View();
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("USUARIO_ID", "");
            HttpContext.Session.SetString("ESCRIBANO", "");
            return RedirectToAction("Login");
        }


    }



}