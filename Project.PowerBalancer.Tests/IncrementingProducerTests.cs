using NUnit.Framework;
using Project.PowerBalancer.Interfaces;
using Project.PowerBalancer.Modules.Clocks;
using Project.PowerBalancer.Modules.PowerSystemConfig;
using Project.PowerBalancer.PowerComponents.Producers;

namespace Project.PowerBalancer.Tests;

public class IncrementingProducerTests
{
    [Test]
    [TestCase(500)]
    [TestCase(0.5)]
    [TestCase(0.1)]
    [TestCase(1)]
    
    public void TestIncrementingProducerCalculation(double maxProduction)
    {
        IClock clock = new TimeClock(0);
        var producer = new IncrementingProducer(maxProduction, clock);

        clock.Start();
        while (clock.IsActive)
        {
            var producedAmount = producer.GetPowerProduction();

            Assert.True(producedAmount <= producer.MaxPowerProduction, "producedAmount <= producer.MaxPowerProduction");
            Assert.True(producedAmount >= 0, "producedAmount >= 0");

            if(clock.Time >= 86000) clock.Deactivate();
        }

    }
}
