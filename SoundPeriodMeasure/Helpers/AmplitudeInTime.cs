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

namespace SoundPeriodMeasure.Helpers
{
    public class AmplitudeInTime
    {
        public AmplitudeInTime(double amplitude, long elapsedMiliseconds)
        {
            Amplitude = amplitude;
            ElapsedMiliseconds = elapsedMiliseconds;
        }

        public AmplitudeInTime() {}

        public double Amplitude;
        public long ElapsedMiliseconds;
    }
}