using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromoDH.Models;

namespace PromoDH.Controllers
{
    public class PreguntaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PreguntaAzar()
        {
            if (HttpContext.Session.GetString("REGISTRO_ID") == null )
                return RedirectToAction("Index","Home");

            PreguntaPromo preg = PreguntaPromo.ObtenerPreguntaAzar();
            if (preg == null)
                return NotFound();

            return View(preg);
        }

        [HttpPost]
        public IActionResult PreguntaAzar([Bind] PreguntaPromo preg)
        {

            if (ModelState.IsValid)
            {
                //if (db.InsertarPais(pais) > 0)
                //{
                //    return RedirectToAction("Index");
                //}
                //else
                //{
                //    ViewBag.Message = pais.errorDesc;
                //}
            }
            return View(preg);
        }


    }
}