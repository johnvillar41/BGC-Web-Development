﻿using System;
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
        private const string SUBJECT = "Code for AGT Confirmation Account";
        private EmailSender()
        {

        }
        public static void BuildEmailSender(string recipientEmail)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(EMAIL, PASSWORD),
                EnableSsl = true,
            };
            var generatedCode = GenerateRandomCode();
            smtpClient.Send(EMAIL, recipientEmail, SUBJECT, generatedCode);
        }
        private static string GenerateRandomCode()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}