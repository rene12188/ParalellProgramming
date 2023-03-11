using NUnit.Framework;
using Project.PowerBalancer.Modules.Clocks;
using Project.PowerBalancer.Modules.PowerSystemConfig;

namespace Project.PowerBalancer.Tests;

public class PowerSystemConfigTests
{
    [Test]
    public void CheckThatPowerSystemConfigIsCreatedCorrectly_NoExceptionThrown()
    {
        var tmp = new RealisticPowerSystemConfig(new TimeClock(1));
    }
}