using Project.PowerBalancer.BaseClasses;
using System;

namespace Project.PowerBalancer.PowerComponents.Consumers
{
    public class SolarAdjustableConsumer : BaseConsumer
    {
        private readonly double _solarPowerFactor;

        public SolarAdjustableConsumer(double maxPowerConsumption, double solarPowerFactor) : base(maxPowerConsumption)
        {
            _solarPowerFactor = solarPowerFactor;
        }

        public override double GetPowerConsumption()
        {
            // Get the current time
            var currentTime = WaitingClock.Time/3600; //IDK HOW TO IMPLEMENT TIME HERE, PLS FIX

            // Determine whether it is daytime
            var isDaytime = currentTime.Hour >= 6 && currentTime.Hour < 18;

            // Calculate the power consumption based on whether it is daytime and the solar power factor
            var powerConsumption = isDaytime ? MaxPowerConsumption * _solarPowerFactor : MaxPowerConsumption;

            return powerConsumption;
        }
    }
}