using System.Net.Mail;

namespace SignUpApi.Service
{
    public interface ISmtpClient
    {
        public void Send(MailMessage data);
    }
}
