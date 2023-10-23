namespace Solution
{
    class CarbonFootprint
    {
        public static void Main(String[] args)
        {
            CarbonFootprint carbonFootprint = new CarbonFootprint();
            Console.WriteLine("Enter the Entity Type Email or Server");
            string entityType=Console.ReadLine();
            if (entityType.Equals("email"))
            {
                int numberOfInboxMail = 0;
                int numberOfSpamMail = 0;
                int numberOfSendMail = 0;
                Console.WriteLine("Enter the inbox mail");
                numberOfInboxMail = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the spam mail");
                numberOfSpamMail = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the send mail");
                numberOfSendMail = Convert.ToInt32(Console.ReadLine());
                double totalCarbonEmission = carbonFootprint.FindCarbonEmissionForEmail(numberOfInboxMail, numberOfSpamMail, numberOfSendMail);
                Console.WriteLine("Carbon Emission is " + totalCarbonEmission + " gram");
            }
            else if(entityType.Equals("server"))
            {
                int duration,powerConsumption;
                double efficiency;
                Console.WriteLine("Enter the duration of Usage in hours");
                duration = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the power consumption in Kilo watts");
                powerConsumption = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the efficiency");
                efficiency = Convert.ToDouble(Console.ReadLine());
                double totalCarbonEmission=carbonFootprint.FindCarbonEmissionForServer(duration, powerConsumption,efficiency);
                Console.WriteLine("Carbon Emission is " + totalCarbonEmission + " gram");
            }
            else
            {
                Console.WriteLine("Enter Correct entity type");
            }
        }
        private double FindCarbonEmissionForEmail(int inbox, int spam, int send)
        {
            return 0.02*(spam + inbox + send);
        }

        private double FindCarbonEmissionForServer(int duration, int power, double efficiency)
        {
            double intensityOfCarbon = 0.5;
            double consumptionOfEnergy = power * duration;
            double emission = (consumptionOfEnergy * intensityOfCarbon);
            emission = emission/efficiency;
            return emission;            
        }

    }
}
