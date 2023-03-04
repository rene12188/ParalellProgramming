using Project.PowerBalancer.BaseClasses;

namespace Project.PowerBalancer.PowerComponents.Producers;

public class SolarProducer : BaseProducer
{
    private readonly Clock _clock;

    public SolarProducer(double maxPowerProduction, Clock clock) : base(maxPowerProduction)
    {
        _clock = clock;
    }

    private static float GetSunlightExposure(int seconds) 
    {
        // Central Europe has approximately 8 hours of daylight per day, or 28,800 seconds
        const int daylightSeconds = 28800;

        // Ensure input is within the valid range
        if (seconds < 0 || seconds > 86400) 
        {
            throw new ArgumentException("Input must be between 0 and 86400");
        }

        // Calculate the proportion of the day that has passed
        float proportionOfDaylight = (float)seconds / daylightSeconds;

        // Ensure the output is between 0 and 1
        return Math.Min(Math.Max(proportionOfDaylight, 0), 1);
    }

    public override double GetPowerProduction()
    {
        return MaxPowerProduction * GetSunlightExposure(_clock._time);
    }
}