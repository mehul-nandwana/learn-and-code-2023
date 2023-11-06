using Solution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Solution.Constants;

namespace Solution
{
    public class CarbonFootprint
    {
        
        public double FindCarbonEmissionForEmail(string source,int inbox, int spam, int send)
        {
            if (source.Equals("gmail"))
                return gmailCarbonIntensity * (spam + inbox + send);
            else if (source.Equals("yahoo")) 
                return yahooCarbonIntensity * (spam + inbox + send);
            else
                return outlookCarbonIntensity * (spam + inbox + send);

        }

        public double FindCarbonEmissionForServer(int duration, int power, double efficiency)
        {          
            double consumptionOfEnergy = power * duration;
            double emission = (consumptionOfEnergy * intensityOfCarbon);
            emission = emission / efficiency;
            return emission;

        }
    }
}
