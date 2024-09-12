using Solution.Models;
using static Solution.Constants;

namespace Solution
{
    public class ServerCarbonFootprintCalculator
    {
        public double GetCarbonEmissionForServer(Server server)
        {
            double consumptionOfEnergy = server.powerConsumption * server.duration;
            double emission = (consumptionOfEnergy * intensityOfCarbon);
            emission = emission / server.efficiency;
            return emission;
        }
    }
}
