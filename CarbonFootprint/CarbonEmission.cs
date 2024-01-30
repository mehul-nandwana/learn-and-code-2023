using Solution;

class CarbonEmission
{
    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Enter the Entity Type Email or Server");
            string entityType = Console.ReadLine().ToLower();
            switch (entityType)
            {
                case "email":
                    EmailCarbonFootprint emailCarbonFootprint = new EmailCarbonFootprint();
                    emailCarbonFootprint.HandleEmail();
                    break;
                case "server":
                    ServerCarbonFootprint serverCarbonFootprint = new ServerCarbonFootprint();
                    serverCarbonFootprint.HandleServer();
                    break;
                default:
                    Console.WriteLine("Enter Correct entity type");
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}