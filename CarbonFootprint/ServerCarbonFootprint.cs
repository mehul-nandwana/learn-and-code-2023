using Solution.Models;
using static Solution.Constants;

namespace Solution
{
    public class ServerCarbonFootprint
    {
        public void HandleServer()
        {
            ServerConfiguration serverEntries = this.GetInputForServer();
            double totalCarbonEmission = this.GetCarbonEmissionForServer(serverEntries);
            this.DisplayCarbonEmissionForServer(totalCarbonEmission);
        }

        public ServerConfiguration GetInputForServer()
        {
            ServerConfiguration server = new ServerConfiguration();
            Console.WriteLine("Enter the duration of Usage in hours");
            server.duration = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the power consumption in Kilo watts");
            server.powerConsumption = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the efficiency");
            server.efficiency = Convert.ToDouble(Console.ReadLine());
            return server;
        }

        public double GetCarbonEmissionForServer(ServerConfiguration server)
        {
            double consumptionOfEnergy = server.powerConsumption * server.duration;
            double emission = (consumptionOfEnergy * intensityOfCarbon);
            emission = emission / server.efficiency;
            return emission;
        }

        public void DisplayCarbonEmissionForServer(double totalCarbonEmission)
        {
            Console.WriteLine("Carbon Emission is " + totalCarbonEmission + " gram");
        }
    }
}
