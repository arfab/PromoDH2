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

        [HttpGet]
        public IActionResult Preguntas()
        {
            return View("Preguntas", Datos.ObtenerPreguntas());
        }

        [HttpGet]
        public IActionResult Ganadores()
        {
            return View("Ganadores", Datos.ObtenerGanadores());
        }


        [HttpGet]
        public IActionResult Consultas()
        {
            return View("Consultas", Datos.ObtenerConsultas("",""));
        }

        [HttpGet]
        public IActionResult Codigos()
        {
            return View("Codigos", Datos.ObtenerRegistros("", -1));
        }


    }

  

}