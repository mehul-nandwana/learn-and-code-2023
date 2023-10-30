using Solution;
using Solution.Models;

class CarbonEmission
    {
        public static void Main(String[] args)
        {
            CarbonFootprint carbonFootprint = new CarbonFootprint();
            Email email = new Email();
            Server server = new Server();
            Console.WriteLine("Enter the Entity Type Email or Server");
            string entityType=Console.ReadLine();
            if (entityType.Equals("email"))
            {
                Console.WriteLine("Enter the Source mail");
                email.source = Console.ReadLine();
                if (email.source.ToLower().Equals("outlook") || email.source.ToLower().Equals("yahoo") || email.source.ToLower().Equals("gmail"))
                {
                    Console.WriteLine("Enter the inbox mail");
                    email.numberOfInboxMail = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter the spam mail");
                    email.numberOfSpamMail = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter the send mail");
                    email.numberOfSendMail = Convert.ToInt32(Console.ReadLine());
                    double totalCarbonEmission = carbonFootprint.FindCarbonEmissionForEmail(email.source,email.numberOfInboxMail, email.numberOfSpamMail, email.numberOfSendMail);
                    Console.WriteLine("Carbon Emission is " + totalCarbonEmission + " gram");
                }
                else
                {
                    Console.WriteLine("Enter Correct source");
                }
            }
            else if(entityType.Equals("server"))
            {
                Console.WriteLine("Enter the duration of Usage in hours");
                server.duration = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the power consumption in Kilo watts");
                server.powerConsumption = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the efficiency");
                server.efficiency = Convert.ToDouble(Console.ReadLine());
                double totalCarbonEmission=carbonFootprint.FindCarbonEmissionForServer(server.duration, server.powerConsumption,server.efficiency);
                Console.WriteLine("Carbon Emission is " + totalCarbonEmission + " gram");
            }
            else
            {
                Console.WriteLine("Enter Correct entity type");
            }
        }
       

    }
