using Solution.Models;
using Solution.Services;
using static Solution.Constants;

namespace Solution
{
    public class EmailCarbonFootprint
    {
        public void HandleEmail()
        {
            EmailEntries emailEntries = this.GetInputForEmail();
            double totalCarbonEmission = this.GetCarbonEmissionForEmail(emailEntries);
            this.DisplayCarbonEmissionForEmail(totalCarbonEmission);
        }

        public EmailEntries GetInputForEmail()
        {
            EmailEntries email = new EmailEntries();
            CarbonFootprintAPIService emailService = new CarbonFootprintAPIService();
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

        public double GetCarbonEmissionForEmail(EmailEntries email)
        {
            return gmailCarbonIntensity * (email.countOfSpamMail + email.countOfSendMail + email.countOfInboxMail);
        }

        public void DisplayCarbonEmissionForEmail(double totalCarbonEmission)
        {       
            Console.WriteLine("Carbon Emission is " + totalCarbonEmission + " gram");
        }
    }
}
