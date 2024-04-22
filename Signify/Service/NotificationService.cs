using System.Net.Mail;
using System.Net;

namespace Signify.Service
{
    public class NotificationService
    {
        public void sendNotification(string email)
        {

            string senderEmail = "kunalvaishnavdav@gmail.com";
            string senderPassword = "onjzedcbzvjwlfaw";

            string recipientEmail = email;

            string subject = "Test Email";
            string body = "Congrats You have signed UpSuccessfully";

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(senderEmail, senderPassword);

            MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail);
            mailMessage.Subject = subject;
            mailMessage.Body = body;

            client.Send(mailMessage);
        }
    }
}
