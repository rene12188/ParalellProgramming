using Project.PowerBalancer;
using Project.PowerBalancer.BaseClasses;
using Project.PowerBalancer.Modules.Clocks;
using Project.PowerBalancer.PowerComponents.Consumers;
using Project.PowerBalancer.PowerComponents.Producers;
using Project.Util.Models.Import;

namespace Project.DataGenerator.Tests;

public class CommunityTests
{
    [Test]
    public void BuyerReport_SellerReport_SingleBuyerAndSeller_StartBalancingWorks()
    {
        //Arrange
        var importCommunity = new List<ImportCommunity>()
        {
            new ImportCommunity("Seller")
            {
                Edges = { new ImportEdge("Seller", "Buyer", 1) }
            },
            new ImportCommunity("Buyer")
        };

        var graphDistanceResolver = new GraphDistanceResolver(importCommunity);
        
        var seller = new Community(new ImportCommunity("Seller"), new List<BaseConsumer>(), new List<BaseProducer>() { new ConstantProducer(100) }, graphDistanceResolver,
            new TimeClock(1000));
        var buyer = new Community(
            new ImportCommunity("Buyer"),
            new List<BaseConsumer>() { new ConstantConsumer(100) },
            new List<BaseProducer>(), graphDistanceResolver,
            new TimeClock(1000));
        graphDistanceResolver.SetCommunities(new List<Community>(){seller, buyer});
        
        //Act
        var sellerThread= new Thread(() => seller.StartBalancingProcess(true));
        sellerThread.Start();
        var buyerThread = new Thread(() => buyer.StartBalancingProcess(true));
        buyerThread.Start();
        Thread.Sleep(5000);
        seller.IsActive= false;
        buyer.IsActive= false;
        
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(seller.PowerSoldReport.Count, Is.EqualTo(1));
            Assert.That(buyer.PowerBoughtReport.Count, Is.EqualTo(1));
        });
        
    }
}