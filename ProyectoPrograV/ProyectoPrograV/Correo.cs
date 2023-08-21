using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Net.Configuration;

namespace ProyectoPrograV
{
    public class Correo
    {
        public void Enviar(string Servidor, List<string> Encargados)
        {
            MailMessage MailMessage;

            foreach (string e in Encargados)
            {
                try
                {
                    MailMessage = new MailMessage("proyectocorreodiego@gmail.com", e);
                    MailMessage.Subject = "Problema en el servidor " + Servidor;
                    MailMessage.Body = "Se han presentado problemas en el servidor " +
                        Servidor + " o unos de sus servicios, por favor verificar";
                    MailMessage.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.UseDefaultCredentials = true;
                    NetworkCredential NetworkCred =
                        new NetworkCredential("proyectocorreodiego@gmail.com", "mecai987");
                    smtp.Credentials = NetworkCred;
                    smtp.EnableSsl = true;
                    smtp.Port = 587;
                    smtp.Send(MailMessage);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}