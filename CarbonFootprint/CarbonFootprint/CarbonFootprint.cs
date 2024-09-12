using Solution.Models;
using System;
using System.Collections.Generic;
using static Solution.Constants;

namespace Solution
{
    public class CarbonFootprint
    {
        public double CalculateCarbonEmissionForEmail(int inbox, int spam, int send)
        {
                return gmailCarbonIntensity * (spam + inbox + send);
        }

        public double CalculateCarbonEmissionForServer(int duration, int power, double efficiency)
        {          
            double consumptionOfEnergy = power * duration;
            double emission = (consumptionOfEnergy * intensityOfCarbon);
            emission = emission / efficiency;
            return emission;
        }
    }
}
