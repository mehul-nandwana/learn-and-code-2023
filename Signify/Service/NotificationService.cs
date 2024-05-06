using System.Net.Mail;
using System.Net;
using SignUpApi.Models;
using SignUpApi.Service;
using Microsoft.Identity.Client;

namespace Signify.Service
{
    public class EmailNotificationService : INotificationService
    {
        private readonly ISmtpClient _smtpClient;
        public EmailNotificationService(ISmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }
        public void sendNotification(string email)
        {
            try
            {
                EmailEntries emailEntries = this.setEmailEntries(email);
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client = this.setClientEntries(emailEntries);
                MailMessage mailMessage = this.setMailData(emailEntries);
                _smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw ex;         
            }
        }

        private EmailEntries setEmailEntries(string email)
        {
            try
            {
                EmailEntries emailEntries = new EmailEntries();
                emailEntries.senderEmail = "kunalvaishnavdav@gmail.com";
                emailEntries.senderPassword = "onjzedcbzvjwlfaw";
                emailEntries.recipientEmail = email;
                return emailEntries;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw ex;
            }
        }

        private SmtpClient setClientEntries(EmailEntries emailEntries)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(emailEntries.senderEmail, emailEntries.senderPassword);
                return client;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw ex;
            }
        }

        private MailMessage setMailData(EmailEntries emailEntries)
        {
            try
            {
                MailMessage mailMessage = new MailMessage(emailEntries.senderEmail, emailEntries.recipientEmail);
                mailMessage.Subject = "Sign Up";
                mailMessage.Body = "Welcome You Have Registered Successfully";
                return mailMessage;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw ex;
            }
        }

        
}
}
