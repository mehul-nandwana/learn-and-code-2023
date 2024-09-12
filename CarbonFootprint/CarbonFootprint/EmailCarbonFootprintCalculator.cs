using Solution.Models;
using static Solution.Constants;

namespace Solution
{
    public class EmailCarbonFootprintCalculator
    {
        public double GetCarbonEmissionForEmail(Email email)
        {
            return gmailCarbonIntensity * (email.countOfSpamMail + email.countOfSendMail + email.countOfInboxMail);
        }
    }
}
