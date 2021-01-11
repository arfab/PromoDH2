using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PromoDH.CapaDatos;
using PromoDH.Models;

namespace PromoDH.Controllers
{
    public class CargaController : Controller
    {
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

            ViewBag.ListOfMarcas = Datos.ObtenerMarcas();
            ViewBag.ListOfProvincias = Datos.ObtenerProvincias();


            return View();
        }

        [HttpPost]
        public IActionResult CargarCodigo([Bind] Registro registro)
        {
            if (ModelState.IsValid)
            {

                // Por ahora hardcodeo la marca hasta que esté en el frontend
                registro.marca_id = 1;
                if (Datos.InsertarRegistro(registro) > 0)
                {
                    // Guardar datos registro y premio en sesión
                    HttpContext.Session.SetString("PREMIO_ID", registro.premio_id_ret.ToString());
                    HttpContext.Session.SetString("REGISTRO_ID", registro.user_id_ret.ToString());
                    HttpContext.Session.SetString("PREMIO_RANGO_ID", registro.premio_rango_id_ret.ToString());

                    if (registro.premio_id_ret > 0)
                        return RedirectToAction("PreguntaAzar", "Pregunta");
                    else
                        return RedirectToAction("NoGano", "Carga");

                    //return Redirect("~/");
                }
            }

            ViewBag.ListOfMarcas = Datos.ObtenerMarcas();
            ViewBag.ListOfProvincias = Datos.ObtenerProvincias();

            return View(registro);
        }


        //[HttpGet]
        //public JsonResult ObtenerProvincias()
        //{
        //    List<Provincia> lpro = Datos.ObtenerProvincias();
        //    return Json(new SelectList(lpro, "id", "descripcion"));
        //}


        //[HttpGet]
        //public JsonResult ObtenerMarcas()
        //{
        //    //List<Marca> lmar = Datos.ObtenerMarcas();

        //    ViewBag.ListOfMarcas = Datos.ObtenerMarcas();

        //    return Json(new SelectList(lmar, "id", "nombre"));
        //}


    }
}