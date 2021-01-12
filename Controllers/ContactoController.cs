using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromoDH.CapaDatos;
using Microsoft.AspNetCore.Mvc;
using PromoDH.Models;

namespace PromoDH.Controllers
{
    public class ContactoController : Controller
    {
        public IActionResult Index()
        {

            ViewBag.ListOfProvincias = Datos.ObtenerProvincias();

            return View();
        }


        [HttpPost]
        public IActionResult Index([Bind] Contacto contacto)
        {
            string sRet;

            sRet = Validar(contacto);

            if (sRet != "")
            {
                ViewBag.Message = sRet;

    
                ViewBag.ListOfProvincias = Datos.ObtenerProvincias();

                return View(contacto);
            }

            if (ModelState.IsValid)
            {

                // Por ahora hardcodeo la marca hasta que esté en el frontend
                //registro.marca_id = 1;



                if (Datos.InsertarConsulta(contacto) > 0)
                {
                        return RedirectToAction("Gracias", "Contacto");
                }
            }

            ViewBag.ListOfProvincias = Datos.ObtenerProvincias();

            return View(contacto);
        }

        public string Validar(Contacto contacto)
        {
            string sRet = "";

            if (contacto.Nombre.Trim().Length > 50) return "El Nombre no debe exceder los 50 caracteres";
            if (contacto.Nombre.Trim().Length == 0) return "Algunos campos están incompletos.";

            if (contacto.Apellido.Trim().Length > 50) return "El Apellido no debe exceder los 50 caracteres";
            if (contacto.Apellido.Trim().Length == 0) return "Algunos campos están incompletos.";

            if (contacto.Dni.Trim().Length == 0) return "Algunos campos están incompletos.";
            /* Falta condicion para que sea numerico*/
            if (contacto.Dni.Trim() == "") return "Algunos campos están incompletos.";
            if (contacto.Dni.Length < 7) return "El DNI no es válido";
            if (contacto.Dni.Length > 8) return "El DNI no es válido";
            if (contacto.Dni.StartsWith("00")) return "El DNI no es válido";
            if (contacto.Dni.StartsWith("111111")) return "El DNI no es válido";
            if (contacto.Dni.StartsWith("222222")) return "El DNI no es válido";
            if (contacto.Dni.StartsWith("333333")) return "El DNI no es válido";
            if (contacto.Dni.StartsWith("444444")) return "El DNI no es válido";
            if (contacto.Dni.StartsWith("555555")) return "El DNI no es válido";
            if (contacto.Dni.StartsWith("666666")) return "El DNI no es válido";
            if (contacto.Dni.StartsWith("777777")) return "El DNI no es válido";
            if (contacto.Dni.StartsWith("888888")) return "El DNI no es válido";
            if (contacto.Dni.StartsWith("999999")) return "El DNI no es válido";
            if (contacto.Dni.StartsWith("123456")) return "El DNI no es válido";
            if (contacto.Dni.StartsWith("987654")) return "El DNI no es válido";

            if (contacto.Consulta.Trim().Length == 0) return "Algunos campos están incompletos.";


            return sRet;

        }

        public IActionResult Gracias()
        {
            return View();
        }

    }
}