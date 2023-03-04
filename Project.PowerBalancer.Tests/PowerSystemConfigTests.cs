using NUnit.Framework;

namespace Project.PowerBalancer.Tests;

public class PowerSystemConfigTests
{
    [Test]
    public void CheckThatPowerSystemConfigIsCreatedCorrectly_NoExceptionThrown()
    {
        var tmp = new PowerSystemConfig(new Clock(1));
    }
}