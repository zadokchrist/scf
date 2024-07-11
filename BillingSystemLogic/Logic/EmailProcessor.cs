using System;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystemLogic.Models;

namespace BillingSystemLogic.Logic
{
    internal class EmailProcessor
    {
        private const string smtpServer = "smtp.gmail.com";
        private const string smtpUsername = "ogunwatercorp@gmail.com";//"ogunwatercorp@gmail.com";
        private const string smtpPassword = "ymujfgxpzdfjxigr";//"OgunWater@2021";

        internal GenericResponse SendEmail1(string Name, string emailAddress, string Subject, string Message)
        {
            GenericResponse response = new GenericResponse();
            try
            {
                string output = "";
                MailMessage message = new MailMessage();
                message.To.Clear();
                SmtpClient mailClient = new SmtpClient(smtpServer);
                MailAddress Email = new MailAddress(emailAddress);
                message.To.Add(emailAddress);
                message.CC.Add(new MailAddress(emailAddress));
                message.Subject = Subject.Replace('\r', ' ').Replace('\n', ' ');
                message.Body = Message;
                message.IsBodyHtml = true;
                message.From = new MailAddress("ngobidaniel05@gmail.com", "LIQUID - " + Name);
                //I USE GMAIL AS THE SMTP SERVER..for more info google
                mailClient.UseDefaultCredentials = true;
                NetworkCredential cred = new NetworkCredential(smtpUsername, smtpPassword, smtpServer);
                mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                mailClient.Timeout = 1000 * 60 * 4;
                mailClient.Credentials = cred;
                mailClient.Port = 587;
                mailClient.EnableSsl = true;
                //SEND EMAIL
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors) { return true; };
                if (!String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(emailAddress))
                    mailClient.Send(message);

                output = "Email successfully delivered";
                response.IsSuccessful = true;
                response.ErrorMessage = "Email successfully delivered";
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        internal GenericResponse SendEmail(string Name, string emailAddress, string Subject, string Message)
        {
            GenericResponse response = new GenericResponse();
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(smtpUsername);
                mail.Sender = new MailAddress(smtpUsername);
                mail.To.Add(emailAddress);
                mail.IsBodyHtml = true;
                mail.Subject = Subject.Replace('\r', ' ').Replace('\n', ' ');
                mail.Body = Message;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(smtpUsername, smtpPassword);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = true;

                smtp.Timeout = 30000;
                try
                {

                    smtp.Send(mail);
                    response.IsSuccessful = true;
                    response.ErrorMessage = "SUCCESS";
                }
                catch (SmtpException e)
                {
                    response.IsSuccessful = false;
                    response.ErrorMessage = e.Message;
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccessful = false;
            }
            return response;
        }

        internal GenericResponse SendEmailWithCopy(string emailAddress, string Subject, string Message, string copyto)
        {
            GenericResponse response = new GenericResponse();
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(smtpUsername);
                mail.Sender = new MailAddress(smtpUsername);
                mail.To.Add(emailAddress);
                mail.CC.Add(copyto);
                mail.IsBodyHtml = true;
                mail.Subject = Subject.Replace('\r', ' ').Replace('\n', ' ');
                mail.Body = Message;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(smtpUsername, smtpPassword);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = true;

                smtp.Timeout = 30000;
                try
                {

                    smtp.Send(mail);
                    response.IsSuccessful = true;
                    response.ErrorMessage = "SUCCESS";
                }
                catch (SmtpException e)
                {
                    response.IsSuccessful = false;
                    response.ErrorMessage = e.Message;
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccessful = false;
            }
            return response;
        }
    }
}
