using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PromoDH.CapaDatos;

namespace PromoDH.Controllers
{
    public class PremioController : Controller
    {
        public IActionResult Index()
        {
            return View(Datos.ObtenerGanadores());
        }
    }
}