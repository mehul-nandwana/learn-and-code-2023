using System.Net.Mail;
using System.Net;
using SignUpApi.Models;

namespace Signify.Service
{
    public class EmailNotificationService: INotificationService
    {
        public void sendNotification(string email)
        {
            EmailEntries emailEntries = this.setEmailEntries(email);
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client = this.setClientEntries(emailEntries);
            MailMessage mailMessage = this.setMailData(emailEntries);
            client.Send(mailMessage);
        }

        public EmailEntries setEmailEntries(string email)
        {
            EmailEntries emailEntries = new EmailEntries();
            emailEntries.senderEmail = "kunalvaishnavdav@gmail.com";
            emailEntries.senderPassword = "onjzedcbzvjwlfaw";
            emailEntries.recipientEmail = email;
            return emailEntries;
        }

        public SmtpClient setClientEntries(EmailEntries emailEntries)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(emailEntries.senderEmail, emailEntries.senderPassword);
            return client;
        }

        public MailMessage setMailData(EmailEntries emailEntries)
        {
            MailMessage mailMessage = new MailMessage(emailEntries.senderEmail,emailEntries.recipientEmail);
            mailMessage.Subject = "Sign Up";
            mailMessage.Body = "Welcome You Have Registered Successfully";
            return mailMessage;
        }
    }
}
