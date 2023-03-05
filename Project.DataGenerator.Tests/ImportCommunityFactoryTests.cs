using Project.Util.DataGenerator;

namespace Project.DataGenerator.Tests;

public class ImportCommunityFactoryTests
{
    [Test]
    public void GenerateCommunity_ReturnListOfCommunities_AssertAllCommunitiesHaveCorrectAmountOfEdges()
    {
        //Arrange
        var dataGenerator = new ImportCommunityFactory();
        DataGeneratorConfig.CreateConfig(dataGenerator);

        //Act
        var result = dataGenerator.GetCommunities();

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Count, Is.EqualTo(14));
            Assert.That(result.Single(c => c.Name == "Bruck-Mürzzuschlag").Edges.Count, Is.EqualTo(4));
            Assert.That(result.Single(c => c.Name == "Deutschlandsberg").Edges.Count, Is.EqualTo(3));
            Assert.That(result.Single(c => c.Name == "Graz").Edges.Count, Is.EqualTo(1));
            Assert.That(result.Single(c => c.Name == "Graz-Umgebung").Edges.Count, Is.EqualTo(9));
            Assert.That(result.Single(c => c.Name == "Hartberg-Fürstenfel").Edges.Count, Is.EqualTo(2));
            Assert.That(result.Single(c => c.Name == "Leibnitz").Edges.Count, Is.EqualTo(3));
            Assert.That(result.Single(c => c.Name == "Leoben").Edges.Count, Is.EqualTo(4));
            Assert.That(result.Single(c => c.Name == "Liezen").Edges.Count, Is.EqualTo(4));
            Assert.That(result.Single(c => c.Name == "Murau").Edges.Count, Is.EqualTo(2));
            Assert.That(result.Single(c => c.Name == "Murtal").Edges.Count, Is.EqualTo(5));
            Assert.That(result.Single(c => c.Name == "Südoststeiermark").Edges.Count, Is.EqualTo(5));
            Assert.That(result.Single(c => c.Name == "Voitsberg").Edges.Count, Is.EqualTo(3));
            Assert.That(result.Single(c => c.Name == "Weiz").Edges.Count, Is.EqualTo(4));
        });
    }
}