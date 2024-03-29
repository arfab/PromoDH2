﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromoDH.CapaDatos;
using Microsoft.AspNetCore.Mvc;
using PromoDH.Models;
using MimeKit;
using System.Collections.Immutable;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using System.IO;
using reCAPTCHA.AspNetCore.Attributes;
using Newtonsoft.Json;
using System.Text;


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
        [ValidateRecaptcha]
        public IActionResult Index([Bind] Contacto contacto)
        {
            string sRet;


            try
            {
                if (ModelState.IsValid)
                {

                // Por ahora hardcodeo la marca hasta que esté en el frontend
                //registro.marca_id = 1;

                sRet = Validar(contacto);

                if (sRet != "")
                {
                    ViewBag.Message = sRet;


                    ViewBag.ListOfProvincias = Datos.ObtenerProvincias();

                    return View(contacto);
                }

                if (Datos.InsertarConsulta(contacto) > 0)
                {
                    IConfigurationBuilder builder = new ConfigurationBuilder()
                                         .SetBasePath(Directory.GetCurrentDirectory())
                                         .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                    IConfigurationRoot configuration = builder.Build();
                    IConfigurationSection SmtpServer = configuration.GetSection("Smtp").GetSection("Server");
                    IConfigurationSection SmtpFrom = configuration.GetSection("Smtp").GetSection("FromAddress");
                    IConfigurationSection SmtpAdmin = configuration.GetSection("Smtp").GetSection("AdminAddress");
                    IConfigurationSection SmtpUser = configuration.GetSection("Smtp").GetSection("User");
                    IConfigurationSection SmtpPassword = configuration.GetSection("Smtp").GetSection("Password");
                    IConfigurationSection SmtpPort = configuration.GetSection("Smtp").GetSection("Port");
                    IConfigurationSection UseSSL = configuration.GetSection("Smtp").GetSection("UseSSL");


                        MimeMessage message = new MimeMessage();
                    MailboxAddress from = new MailboxAddress("Admin", SmtpFrom.Value);
                    //MailboxAddress to = new MailboxAddress(contacto.Nombre + ", " + contacto.Apellido, contacto.Email);

                    MailboxAddress to = new MailboxAddress("Arcor", SmtpAdmin.Value);
                    message.Subject = "Consulta desde el Sitio Promo TOPLINE LOLLAPALOOZA";
                    message.To.Add(to);
                    message.From.Add(from);

                    string nl = "<br/>";
                    string nl2 = System.Environment.NewLine;

                    String sHtmlBody = "";
                    String sTextBody = "";

                    sHtmlBody = sHtmlBody + "*************************************************************************" + nl;
                    sHtmlBody = sHtmlBody + "Email de consulta desde el Sitio Promo TOPLINE LOLLAPALOOZA" + nl ;
                    sHtmlBody = sHtmlBody + "*************************************************************************" + nl + nl;
                    sHtmlBody = sHtmlBody + "Datos de la consulta:" + nl;
                    sHtmlBody = sHtmlBody + "Nombre y Apellido: " + contacto.Nombre.Trim() + " " +  contacto.Apellido.Trim() + nl;
                    sHtmlBody = sHtmlBody + "Dni: " + contacto.Dni + nl;
                    sHtmlBody = sHtmlBody  + "Direccion: " + contacto.Direccion + nl;
                    sHtmlBody = sHtmlBody  + "Email: " + contacto.Email + nl;
                    sHtmlBody = sHtmlBody  + "Ciudad: " + contacto.Localidad + nl;
                    sHtmlBody = sHtmlBody  + "Provincia: " + contacto.Provincia + nl;
                    sHtmlBody = sHtmlBody + "Telefono: " + contacto.Area + "-" + contacto.Telefono + nl;
                    sHtmlBody = sHtmlBody + "Movil: " + contacto.AreaMovil + "-" + contacto.Movil + nl;
                    sHtmlBody = sHtmlBody + "Comentarios: " + contacto.Consulta + nl ;

                    sTextBody = sTextBody + "*************************************************************************" + nl2;
                    sTextBody = sTextBody + "Email de consulta desde el Sitio Promo TOPLINE LOLLAPALOOZA" + nl2;
                    sTextBody = sTextBody + "*************************************************************************" + nl2 + nl2;
                    sTextBody = sTextBody + "Datos de la consulta:" + nl2;
                    sTextBody = sTextBody + "Nombre y Apellido: " + contacto.Nombre.Trim() + " " + contacto.Apellido.Trim() + nl2;
                    sTextBody = sTextBody + "Dni: " + contacto.Dni + nl2;
                    sTextBody = sTextBody + "Direccion: " + contacto.Direccion + nl2;
                    sTextBody = sTextBody + "Email: " + contacto.Email + nl2;
                    sTextBody = sTextBody + "Ciudad: " + contacto.Localidad + nl2;
                    sTextBody = sTextBody + "Provincia: " + contacto.Provincia + nl2;
                    sTextBody = sTextBody + "Telefono: " + contacto.Area + "-" + contacto.Telefono + nl2;
                    sTextBody = sTextBody + "Movil: " + contacto.AreaMovil + "-" + contacto.Movil + nl2;
                    sTextBody = sTextBody + "Comentarios: " + contacto.Consulta + nl2;


                    BodyBuilder bodyBuilder = new BodyBuilder();
                    bodyBuilder.HtmlBody = sHtmlBody;
                    bodyBuilder.TextBody = sTextBody;
                    message.Body = bodyBuilder.ToMessageBody();

                    SmtpClient client = new SmtpClient();
                    //client.Connect(SmtpServer.Value, 25, false);
                    client.Connect(SmtpServer.Value, Int32.Parse(SmtpPort.Value), bool.Parse(UseSSL.Value));
                    client.Authenticate(SmtpUser.Value, SmtpPassword.Value);

                    client.Send(message);
                    client.Disconnect(true);
                    client.Dispose();




                    return RedirectToAction("Gracias", "Contacto");
                }

                else
                {
                    ViewBag.Message = "Algunos campos estan incompletos.";
                }
            }


            //ME FIJO SI EL PROBLEMA ES EL CAPTCHA
            var state = ViewData.ModelState.FirstOrDefault(x => x.Key.Equals("Recaptcha"));
            if (state.Value != null && state.Value.Errors.Any(x => !string.IsNullOrEmpty(x.ErrorMessage)))
            {
                ViewBag.Message = "Revise el CAPTCHA.";

            }
            else
            {
                ViewBag.Message = "Hubo un problema con el formulario, por favor revise los campos.";

            }



            }
            catch (Exception)
            {
                ViewBag.Message = "Ha ocurrido un error en el sitio. Inténtelo nuevamente.";
                // return RedirectToAction("Index", "Home");
            }


    ViewBag.ListOfProvincias = Datos.ObtenerProvincias();

            return View(contacto);
        }

        public string Validar(Contacto contacto)
        {
            string sRet = "";

            //object m = null;
            //string s = m.ToString();

            if (contacto.Nombre.Trim().Length > 50) return "El Nombre no debe exceder los 50 caracteres";
            if (contacto.Nombre.Trim().Length == 0) return "Algunos campos están incompletos.";

            if (contacto.Apellido.Trim().Length > 50) return "El Apellido no debe exceder los 50 caracteres";
            if (contacto.Apellido.Trim().Length == 0) return "Algunos campos están incompletos.";

            if (contacto.Dni.Trim().Length == 0) return "Algunos campos están incompletos.";
            if (!int.TryParse(contacto.Dni, out _)) return "El DNI no es válido";
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