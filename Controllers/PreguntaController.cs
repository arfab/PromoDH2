using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromoDH.CapaDatos;
using PromoDH.Models;
using MimeKit;
using System.Collections.Immutable;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics.CodeAnalysis;

namespace PromoDH.Controllers
{
    public class PreguntaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

       

        public IActionResult Gano()
        {

            if (HttpContext.Session.GetString("PREMIO_ID") == null)
                return RedirectToAction("Index", "Home");

            if (HttpContext.Session.GetString("PREMIO_ID") == "0")
                return RedirectToAction("Index", "Home");

        

            return View();
        }

        public IActionResult Perdio()
        {
            if (HttpContext.Session.GetString("PREMIO_ID") == null)
                return RedirectToAction("Index", "Home");

            return View();
        }

        public IActionResult Tarde()
        {
            if (HttpContext.Session.GetString("PREMIO_ID") == null)
                return RedirectToAction("Index", "Home");

            if (HttpContext.Session.GetString("PREMIO_ID") == "0")
                return RedirectToAction("Index", "Home");


            return View();
        }


        [HttpGet]
        public IActionResult PreguntaAzar()
        {
            if (HttpContext.Session.GetString("REGISTRO_ID") == null )
                return RedirectToAction("Index","Home");

            PreguntaPromo preg = Datos.ObtenerPreguntaAzar();
            if (preg == null)
                return NotFound();

            return View(preg);
        }

        [HttpPost]
        public IActionResult PreguntaAzar([Bind] PreguntaPromo preg)
        {
            int iPremio;
            string sError ;

            if (ModelState.IsValid)
            {
                
                if (Datos.InsertarRespuesta(preg, Int16.Parse(HttpContext.Session.GetString("REGISTRO_ID")), Int16.Parse(HttpContext.Session.GetString("PREMIO_RANGO_ID"))) > 0)
                {
                    if (preg.rsel==preg.rc)
                    {
                        Datos.InsertarPremio(Int16.Parse(HttpContext.Session.GetString("REGISTRO_ID")), Int16.Parse(HttpContext.Session.GetString("PREMIO_RANGO_ID")), out iPremio, out sError);

                        if (iPremio>0)
                        {
                            HttpContext.Session.SetString("PREMIO_ID", iPremio.ToString());
                            EnviarMail(iPremio);

                            return RedirectToAction("CargarGanador", "Carga");


                        }
                        else
                        {
                            return RedirectToAction("Tarde", "Pregunta");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Perdio", "Pregunta");
                    }
                   
                }
                else
                {
                    ViewBag.Message = preg.errorDesc;
                }
            }
            return View(preg);
        }


        public string EnviarMail(int iPremio)
        {

            string sPremio = "";


            try
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


                if (iPremio == 1)
                    sPremio = "Combo Campamento";
                else
                    if (iPremio == 2)
                    sPremio = "Combo Paseo";
                else
                    if (iPremio == 3)
                    sPremio = "Combo Gamer";
                else
                    if (iPremio == 4)
                    sPremio = "Combo Tarde en Casa";

                MimeMessage message = new MimeMessage();
                MailboxAddress from = new MailboxAddress("Admin", SmtpFrom.Value);
                MailboxAddress to = new MailboxAddress("Admin", SmtpAdmin.Value);
                message.Subject = "Premio en el Sitio Promo TOPLINE LOLLAPALOOZA";
                message.To.Add(to);
                message.From.Add(from);

                string nl = "<br/>";
                string nl2 = System.Environment.NewLine;

                String sHtmlBody = "";
                String sTextBody = "";

                sHtmlBody = sHtmlBody + "*************************************************************************" + nl;
                sHtmlBody = sHtmlBody + "Se ha entregado un premio sitio Promo TOPLINE LOLLAPALOOZA" + nl;
                sHtmlBody = sHtmlBody + "*************************************************************************" + nl + nl;
                sHtmlBody = sHtmlBody + "Código:" + HttpContext.Session.GetString("CODIGO").ToString() + nl;
                sHtmlBody = sHtmlBody + "Dni: " + HttpContext.Session.GetString("DNI").ToString() + nl;
                sHtmlBody = sHtmlBody + "Premio: " + sPremio + nl;
                sHtmlBody = sHtmlBody + "*************************************************************************" + nl;
                sHtmlBody = sHtmlBody + "Fecha: " + DateTime.Now.ToString() + nl;
                sHtmlBody = sHtmlBody + "*************************************************************************" + nl + nl;

                sTextBody = sTextBody + "*************************************************************************" + nl;
                sTextBody = sTextBody + "Se ha entregado un premio sitio Promo TOPLINE LOLLAPALOOZA" + nl;
                sTextBody = sTextBody + "*************************************************************************" + nl + nl;
                sTextBody = sTextBody + "Código:" + HttpContext.Session.GetString("CODIGO").ToString() + nl;
                sTextBody = sTextBody + "Dni: " + HttpContext.Session.GetString("DNI").ToString() + nl;
                sTextBody = sTextBody + "Premio: " + sPremio + nl;
                sTextBody = sTextBody + "*************************************************************************" + nl;
                sTextBody = sTextBody + "Fecha: " + DateTime.Now.ToString() + nl;
                sTextBody = sTextBody + "*************************************************************************" + nl + nl;


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

                return "";

            }
            catch (Exception e)
            {
                return "No se pudo enviar el email: " + e.Message.ToString();
            }

        }
     }
 }