using System.Collections.Generic;
using System.Linq;
using SoundPeriodMeasure.SmallClasses;

namespace SoundPeriodMeasure.Helpers
{
    public static class TextFormatter
    {
        public static string FormatAsResults(IList<AmplitudeInTime> measures)
        {       
            var periods = DataAnalyser.CreatePeriods(measures);
            if (!periods.Any())
            {
                return "No periods found.";
            }

            string result = "Periods between maximas:\n";
            foreach (var period in periods)
            {
                result = result + period + "\n";
            }
                      
            return result;
        }
    }
}