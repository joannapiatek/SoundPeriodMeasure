namespace SoundPeriodMeasure.SmallClasses
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