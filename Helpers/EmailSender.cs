using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace SoftEngWebEmployee.Helpers
{
    public class EmailSender
    {
        private const string EMAIL = "jwca.mcl2020@gmail.com";
        private const string PASSWORD = "passwordmcl";
        private static string SUBJECT = $"Code for {Constants.BGC} Confirmation Account";
        private EmailSender()
        {

        }
        public static void BuildEmailSender(string recipientEmail,string generatedCode)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(EMAIL, PASSWORD),
                Timeout = 20000
            };
            
            using (var message = new MailMessage(EMAIL, recipientEmail)
            {
                Subject = SUBJECT,
                Body = generatedCode
            })
            {
                smtp.Send(message);
            }           
        }
        public static string GenerateRandomCode()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}