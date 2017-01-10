using System;
using System.Collections.Generic;
using System.Linq;
using SoundPeriodMeasure.SmallClasses;

namespace SoundPeriodMeasure.Helpers
{
    public static class DataAnalyser
    {
        public static List<AmplitudeInTime> FindMaximas(AmplitudeInTime[] values)
        {
            int monotonicLength = 4;
            double [] monotonic = new double[monotonicLength];
            var maximas = new List<AmplitudeInTime>();

            for (int i = 0; i < values.Length - monotonicLength; i++)
            {
                if (values[i + monotonicLength] == null)
                {
                    break;
                }               

                for (int j = 0; j < monotonicLength; j++)
                {
                    monotonic[j] = values[i + j].Amplitude;
                }

                if (monotonic[0] < monotonic[1]
                    && monotonic[1] > monotonic[2]
                    && monotonic[2] > monotonic[3])
                {
                    maximas.Add(values[i+1]);
                }
            }

            if (maximas.Count > 2)
            {
                var biggestMax = maximas.Select(m => m.Amplitude).Max();
                var cutLevel = biggestMax*0.4;
                maximas = maximas.Where(m => m.Amplitude > cutLevel).ToList();
            }

            return maximas;
        }

        public static List<SoundPeriod> CreatePeriods(IList<AmplitudeInTime> amplitudes)
        {
            var periods = new List<SoundPeriod>();

            for (int i = 0; i < amplitudes.Count-1; i++)
            {
                var meas = Math.Abs(amplitudes[i].ElapsedMiliseconds - amplitudes[i + 1].ElapsedMiliseconds);
                var descr = (i+1) + " and " + (i + 2) + ": " ;

                var period = new SoundPeriod(meas, descr);
                periods.Add(period);
            }

            return periods;
        }
    }
}