using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using PromoDH.CapaDatos;

namespace PromoDH.Controllers
{
    public class TestController : Controller
    {
        public IActionResult PruebaBase()
        {

            try
            {
                Datos.ObtenerGanadores();
                ViewBag.Message = "Conexión a la Base de datos con EXITO!";
            }
            catch (Exception e)
            {
                ViewBag.Message = "Error al conectarse a la Base de Datos. " + e.Message;
            }

            return View();
        }

        public IActionResult PruebaMail()
        {

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


                MimeMessage message = new MimeMessage();
                MailboxAddress from = new MailboxAddress("Admin", SmtpFrom.Value);
                MailboxAddress to = new MailboxAddress("Arcor", SmtpAdmin.Value);
                message.Subject = "Email de prueba desde el Sitio Promo A Todo Bagley 2021";
                message.To.Add(to);
                message.From.Add(from);

                string nl = "<br/>";
                string nl2 = System.Environment.NewLine;

                String sHtmlBody = "";
                String sTextBody = "";

                sHtmlBody = sHtmlBody + "*************************************************************************" + nl;
                sHtmlBody = sHtmlBody + "Email de prueba desde el Sitio Promo A Todo Bagley 2021" + nl;
                sHtmlBody = sHtmlBody + "*************************************************************************" + nl + nl;
               
                sTextBody = sTextBody + "*************************************************************************" + nl2;
                sTextBody = sTextBody + "Email de prueba desde el Sitio Promo A Todo Bagley 2021" + nl2;
                sTextBody = sTextBody + "*************************************************************************" + nl2 + nl2;
               

                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = sHtmlBody;
                bodyBuilder.TextBody = sTextBody;
                message.Body = bodyBuilder.ToMessageBody();

                SmtpClient client = new SmtpClient();

                client.Connect(SmtpServer.Value, Int32.Parse(SmtpPort.Value), bool.Parse(UseSSL.Value));
                client.Authenticate(SmtpUser.Value, SmtpPassword.Value);

                client.Send(message);
                client.Disconnect(true);
                client.Dispose();

                ViewBag.Message = "Email enviado, por favor chequear en casilla de destino.";
            }
            catch (Exception e)
            {
                ViewBag.Message = "El email no ha podido ser enviado. " + e.Message;
            }

            return View();
        }
    }
}