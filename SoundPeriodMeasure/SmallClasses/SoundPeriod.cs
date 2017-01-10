namespace SoundPeriodMeasure.SmallClasses
{
    public class SoundPeriod
    {
        public string Description { get; set; }
        public long Measure { get; set; }

        public SoundPeriod(long measure, string description)
        {
            Measure = measure;
            Description = description;
        }

        public override string ToString()
        {
            return Description + Measure + " ms";
        }
    }
}