using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
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
            int? pag_codigos = HttpContext.Session.GetInt32("PAG_CODIGOS");
            int cant = Datos.ObtenerRegistrosCantidad();

            HttpContext.Session.SetInt32("TOT_PAG_CODIGOS", 10);
            HttpContext.Session.SetInt32("TOT__CODIGOS", cant);

            //int? tot_pag_codigos = HttpContext.Session.GetInt32("TOT_PAG_CODIGOS");
            HttpContext.Session.SetInt32("TOT_PAG_CODIGOS", cant % 25 == 0 ? cant / 25 : cant / 25 + 1);


            if (pag_codigos == null)
            {
                pag_codigos = 1;
                HttpContext.Session.SetInt32("PAG_CODIGOS", 1);
            }

            if (HttpContext.Session.GetInt32("USUARIO_ID").GetValueOrDefault() != 0)
                return View("Codigos", Datos.ObtenerRegistrosPag("", -1, pag_codigos,25));
            else
                return View("Login");
        }

        [HttpGet]
        public IActionResult CodigosSiguiente()
        {
            int pag_codigos = HttpContext.Session.GetInt32("PAG_CODIGOS").GetValueOrDefault();

            HttpContext.Session.SetInt32("PAG_CODIGOS", pag_codigos + 1);

            return RedirectToAction("Codigos");
        }

        [HttpGet]
        public IActionResult CodigosAnterior()
        {
            int pag_codigos = HttpContext.Session.GetInt32("PAG_CODIGOS").GetValueOrDefault();

            HttpContext.Session.SetInt32("PAG_CODIGOS", pag_codigos - 1);

            return RedirectToAction("Codigos");
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


        public IActionResult DescargarCodigos()
        {
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "Codigos.xlsx";

            List<Registro> lGrilla = Datos.ObtenerRegistros("", -1);

            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet =
                    workbook.Worksheets.Add("Codigos");
                    worksheet.Cell(1, 1).Value = "Id";
                    worksheet.Cell(1, 2).Value = "Email";
                    worksheet.Cell(1, 3).Value = "Nombre";
                    worksheet.Cell(1, 4).Value = "Apellido";
                    worksheet.Cell(1, 5).Value = "Fecha";
                    worksheet.Cell(1, 6).Value = "Direccion";
                    worksheet.Cell(1, 7).Value = "Ciudad";
                    worksheet.Cell(1, 8).Value = "Provincia";
                    worksheet.Cell(1, 9).Value = "Telefono";
                    worksheet.Cell(1, 10).Value = "Codigo";
                    for (int index = 1; index <= lGrilla.Count; index++)
                    {
                        worksheet.Cell(index + 1, 1).Value = lGrilla[index - 1].Id;
                        worksheet.Cell(index + 1, 2).Value = lGrilla[index - 1].Email;
                        worksheet.Cell(index + 1, 3).Value = lGrilla[index - 1].Nombre;
                        worksheet.Cell(index + 1, 4).Value = lGrilla[index - 1].Apellido;
                        worksheet.Cell(index + 1, 5).Value = lGrilla[index - 1].fecha_str;
                        worksheet.Cell(index + 1, 6).Value = lGrilla[index - 1].Direccion;
                        worksheet.Cell(index + 1, 7).Value = lGrilla[index - 1].Localidad;
                        worksheet.Cell(index + 1, 9).Value = lGrilla[index - 1].Provincia;
                        worksheet.Cell(index + 1, 10).Value = lGrilla[index - 1].codigo;
                    }
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, contentType, fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                return  View("Codigos", Datos.ObtenerRegistros("", -1)); 
            }
          

        }


        public IActionResult DescargarConsultas()
        {
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "Consultas.xlsx";

            List<Contacto> lGrilla = Datos.ObtenerConsultas("", "");

            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet =
                    workbook.Worksheets.Add("Codigos");
                    worksheet.Cell(1, 1).Value = "Id";
                    worksheet.Cell(1, 2).Value = "Email";
                    worksheet.Cell(1, 3).Value = "Nombre";
                    worksheet.Cell(1, 4).Value = "Apellido";
                    worksheet.Cell(1, 5).Value = "Fecha";
                    worksheet.Cell(1, 6).Value = "Direccion";
                    worksheet.Cell(1, 7).Value = "Ciudad";
                    worksheet.Cell(1, 8).Value = "Provincia";
                    worksheet.Cell(1, 9).Value = "Telefono";
                    worksheet.Cell(1, 9).Value = "Movil";
                    worksheet.Cell(1, 10).Value = "Consulta";
                    for (int index = 1; index <= lGrilla.Count; index++)
                    {
                        worksheet.Cell(index + 1, 1).Value = lGrilla[index - 1].Id;
                        worksheet.Cell(index + 1, 2).Value = lGrilla[index - 1].Email;
                        worksheet.Cell(index + 1, 3).Value = lGrilla[index - 1].Nombre;
                        worksheet.Cell(index + 1, 4).Value = lGrilla[index - 1].Apellido;
                        worksheet.Cell(index + 1, 5).Value = lGrilla[index - 1].fecha;
                        worksheet.Cell(index + 1, 6).Value = lGrilla[index - 1].Direccion;
                        worksheet.Cell(index + 1, 7).Value = lGrilla[index - 1].Localidad;
                        worksheet.Cell(index + 1, 9).Value = lGrilla[index - 1].Provincia;
                        worksheet.Cell(index + 1, 10).Value = lGrilla[index - 1].Consulta;
                    }
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, contentType, fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Codigos", Datos.ObtenerRegistros("", -1));
            }


        }



    }



}