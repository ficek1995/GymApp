using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace GymApp.Services
{
    public class MailService
    {
        private const string _companyEmail = "gymappwebmaster@gmail.com";
        private const string _password = "zaq1@WSX";
        public static void SendMail(string title, string text, string addressee)
        {
            var msg = new MailMessage(_companyEmail, addressee, title, text);
            msg.To.Add(addressee);
            msg.IsBodyHtml = true;
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new NetworkCredential(_companyEmail, _password);
            smtpClient.EnableSsl = true;
            smtpClient.Send(msg);
        }
    }
}