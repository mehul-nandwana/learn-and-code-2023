using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Routing;
using Moq;
using NUnit.Framework;
using Signify.Service;
using SignUpApi.Models;
using System.Net.Mail;

namespace SignUpApi.Test
{
    public class EmailNotificationServiceTests
    {
        private Mock<SmtpClient> _smtpClientMock;
        private EmailNotificationService _emailNotificationService;

        [SetUp]
        public void SetUp()
        {
            _smtpClientMock = new Mock<SmtpClient>("smtp.gmail.com")
            {
                CallBase = true
            };

            _emailNotificationService = new EmailNotificationService();
        }

        [Test]
        public void sendNotification_ValidEmail_SendsEmail()
        {
            // Arrange
            var email = "test@example.com";
            var emailEntries = new EmailEntries
            {
                senderEmail = "kunalvaishnavdav@gmail.com",
                senderPassword = "onjzedcbzvjwlfaw",
                recipientEmail = email
            };
            var mailMessage = new MailMessage(emailEntries.senderEmail, emailEntries.recipientEmail)
            {
                Subject = "Sign Up",
                Body = "Welcome You Have Registered Successfully"
            };

            _smtpClientMock.Setup(s => s.Send(It.IsAny<MailMessage>())).Verifiable();

            // Act
            _emailNotificationService.sendNotification(email);

            // Assert
            _smtpClientMock.Verify(s => s.Send(It.Is<MailMessage>(m =>
                m.From.Address == emailEntries.senderEmail &&
                m.To[0].Address == emailEntries.recipientEmail &&
                m.Subject == "Sign Up" &&
                m.Body == "Welcome You Have Registered Successfully"
            )), Times.Once);
        }

    }
}
