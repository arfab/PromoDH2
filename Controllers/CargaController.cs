using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromoDH.Models;

namespace PromoDH.Controllers
{
    public class CargaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CargarCodigo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CargarCodigo([Bind] Registro registro)
        {
            if (ModelState.IsValid)
            {
                if (Registro.InsertarCodigo(registro) > 0)
                {
                    // Guardar datos registro y premio en sesión
                    HttpContext.Session.SetString("PREMIO_ID", registro.premio_id_ret.ToString());
                    HttpContext.Session.SetString("REGISTRO_ID", registro.user_id_ret.ToString());
                    HttpContext.Session.SetString("PREMIO_RANGO_ID", registro.premio_rango_id_ret.ToString());

                    if (registro.premio_id_ret > 0)
                        return RedirectToAction("PreguntaAzar", "Pregunta");
                    else
                        return RedirectToAction("Index", "Perdedor");

                    //return Redirect("~/");
                }
            }
            return View(registro);
        }

    }
}