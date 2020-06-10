using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                    return Redirect("~/");
                }
            }
            return View(registro);
        }

    }
}