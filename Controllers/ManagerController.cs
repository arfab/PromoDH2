using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PromoDH.CapaDatos;

namespace PromoDH.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Premios()
        {
            return View("Premios",  Datos.ObtenerSembrado());
        }

    }

  

}