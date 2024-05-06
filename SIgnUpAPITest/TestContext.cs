using Moq;
using Signify.Service;
using SignUpApi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;


namespace SIgnUpAPITest
{
    public class TestContext
    {
        public Mock<ISmtpClient> _smtpClientMock = new Mock<ISmtpClient>();
        //public Mock<EmailNotificationService> _emailNotificationService = new Mock<EmailNotificationService>();
    }
}
