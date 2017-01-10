using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Media;

namespace SoundPeriodMeasure.Helpers
{
    public static class DataAnalyser
    {
        public static List<AmplitudeInTime> FindMaximas(AmplitudeInTime[] values)
        {
            int monotonicLength = 5;
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
                    && monotonic[2] > monotonic[3]
                    && monotonic[3] > monotonic[4])
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
    }
}