using NUnit.Framework;
using Project.DataGenerator;

namespace Project.PowerBalancer.Tests;

public class GraphDistanceResolverTests
{
    [Test]
    public void GetDistance_2ConnectedNodes_DistanceCorrect()
    {
        // Arrange
        ImportCommunityFactory importCommunityFactory = new();

        importCommunityFactory.AddCommunity("C1");
        importCommunityFactory.AddCommunity("C2");

        importCommunityFactory.AddEdge("C1", "C2", 1);

        //Act
        var result = new GraphDistanceResolver(importCommunityFactory.GetCommunities()).GetDistancesFromSource("C1");

        //Assert

        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.FirstOrDefault().Value, Is.EqualTo(1.0));
    }

    [Test]
    public void GetDistance_3NodesStringedTogether_DistanceCorrect()
    {
        // Arrange
        ImportCommunityFactory importCommunityFactory = new();

        importCommunityFactory.AddCommunity("C1");
        importCommunityFactory.AddCommunity("C2");
        importCommunityFactory.AddCommunity("C3");

        importCommunityFactory.AddEdge("C1", "C2", 1);
        importCommunityFactory.AddEdge("C2", "C3", 1);

        //Act
        var result = new GraphDistanceResolver(importCommunityFactory.GetCommunities()).GetDistancesFromSource("C1");

        //Assert

        Assert.That(result.Count(), Is.EqualTo(2));
        Assert.That(result["C2"], Is.EqualTo(1.0));
        Assert.That(result["C3"], Is.EqualTo(2.0));
    }
}