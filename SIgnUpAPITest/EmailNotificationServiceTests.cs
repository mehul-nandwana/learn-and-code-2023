using Moq;
using NUnit.Framework;
using Signify.Service;
using SignUpApi.Models;
using System.Net.Mail;
using Xunit;

namespace SIgnUpAPITest
{
    public class EmailNotificationServiceTests
    {

        [Fact]
        public void sendNotification_ValidEmail_SendsEmail()
        {
            TestContext context = new TestContext();
            // Arrange
            string email = "test@example.com";
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

            context._smtpClientMock.Setup(s => s.Send(It.IsAny<MailMessage>())).Verifiable();
            EmailNotificationService emailNotificationService = GetEmailNotificationService(context);

            // Act
            emailNotificationService.sendNotification(email);

            // Assert
            context._smtpClientMock.Verify(s => s.Send(It.Is<MailMessage>(m =>
                m.From.Address == emailEntries.senderEmail &&
                m.To[0].Address == emailEntries.recipientEmail &&
                m.Subject == "Sign Up" &&
                m.Body == "Welcome You Have Registered Successfully"
            )), Times.Once);
        }
        public EmailNotificationService GetEmailNotificationService(TestContext context)
        {
            return new EmailNotificationService(context._smtpClientMock.Object);
        }
    }
}
