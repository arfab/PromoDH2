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
    public class PreguntaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Gano()
        {
            return View();
        }

        public IActionResult Perdio()
        {
            return View();
        }

        public IActionResult Tarde()
        {
            return View();
        }


        [HttpGet]
        public IActionResult PreguntaAzar()
        {
            /*if (HttpContext.Session.GetString("REGISTRO_ID") == null )
                return RedirectToAction("Index","Home");*/

            PreguntaPromo preg = Datos.ObtenerPreguntaAzar();
            if (preg == null)
                return NotFound();

            return View(preg);
        }

        [HttpPost]
        public IActionResult PreguntaAzar([Bind] PreguntaPromo preg)
        {
            int iPremio;
            string sError ;

            if (ModelState.IsValid)
            {
                
                if (Datos.InsertarRespuesta(preg, Int16.Parse(HttpContext.Session.GetString("REGISTRO_ID")), Int16.Parse(HttpContext.Session.GetString("PREMIO_RANGO_ID"))) > 0)
                {
                    if (preg.rsel==preg.rc)
                    {
                        Datos.InsertarPremio(Int16.Parse(HttpContext.Session.GetString("REGISTRO_ID")), Int16.Parse(HttpContext.Session.GetString("PREMIO_RANGO_ID")), out iPremio, out sError);

                        if (iPremio>0)
                        {
                            return RedirectToAction("Gano", "Pregunta");
                        }
                        else
                        {
                            return RedirectToAction("Tarde", "Pregunta");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Perdio", "Pregunta");
                    }
                   
                }
                else
                {
                    ViewBag.Message = preg.errorDesc;
                }
            }
            return View(preg);
        }


    }
}