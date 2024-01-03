using Solution.Models;
using Solution.Services;
using static Solution.Constants;

namespace Solution
{
    public class EmailCarbonFootprint
    {
        public void HandleEmail()
        {
            EmailCarbonFootprint emailCarbonFootprint = new EmailCarbonFootprint();
            Email emailEntries = emailCarbonFootprint.GetInputForEmail();
            double totalCarbonEmission = emailCarbonFootprint.GetCarbonEmissionForEmail(emailEntries);
            emailCarbonFootprint.DisplayCarbonEmissionForEmail(totalCarbonEmission);
        }

        public Email GetInputForEmail()
        {
            EmailCarbonFootprint emailCarbonFootprintCalculator = new EmailCarbonFootprint();
            Email email = new Email();
            CarbonEmission carbonEmission = new CarbonEmission();
            CarbonFootprintService emailService = new CarbonFootprintService();
            string dummyEmail = "kunalvaishnavdav@gmail.com";
            string dummyEmailPassword = "onjzedcbzvjwlfaw";
            string spamFolderPath = "[Gmail]/Spam";
            string sentMailFolderPath = "[Gmail]/Sent Mail";
            string inboxFolderPath = "INBOX";
            email.username = dummyEmail;
            email.password = dummyEmailPassword;
            email.countOfSpamMail = emailService.GetCountOfMessages(email, spamFolderPath);
            email.countOfSendMail = emailService.GetCountOfMessages(email, sentMailFolderPath);
            email.countOfInboxMail = emailService.GetCountOfMessages(email, inboxFolderPath);
            return email;
        }

        public double GetCarbonEmissionForEmail(Email email)
        {
            return gmailCarbonIntensity * (email.countOfSpamMail + email.countOfSendMail + email.countOfInboxMail);
        }

        public void DisplayCarbonEmissionForEmail(double totalCarbonEmission)
        {       
            Console.WriteLine("Carbon Emission is " + totalCarbonEmission + " gram");
        }
    }
}
