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
            Assert.That(result.Count, Is.EqualTo(13));
            Assert.That(result.Single(c => c.Name == "Bruck-Mürzzuschlag").GetEdges.Count, Is.EqualTo(4));
            Assert.That(result.Single(c => c.Name == "Deutschlandsberg").GetEdges.Count, Is.EqualTo(3));
            Assert.That(result.Single(c => c.Name == "Graz").GetEdges.Count, Is.EqualTo(1));
            Assert.That(result.Single(c => c.Name == "Graz-Umgebung").GetEdges.Count, Is.EqualTo(9));
            Assert.That(result.Single(c => c.Name == "Hartberg-Fürstenfel").GetEdges.Count, Is.EqualTo(2));
            Assert.That(result.Single(c => c.Name == "Leibnitz").GetEdges.Count, Is.EqualTo(3));
            Assert.That(result.Single(c => c.Name == "Leoben").GetEdges.Count, Is.EqualTo(4));
            Assert.That(result.Single(c => c.Name == "Liezen").GetEdges.Count, Is.EqualTo(4));
            Assert.That(result.Single(c => c.Name == "Murau").GetEdges.Count, Is.EqualTo(2));
            Assert.That(result.Single(c => c.Name == "Murtal").GetEdges.Count, Is.EqualTo(5));
            Assert.That(result.Single(c => c.Name == "Südoststeiermark").GetEdges.Count, Is.EqualTo(4));
            Assert.That(result.Single(c => c.Name == "Voitsberg").GetEdges.Count, Is.EqualTo(3));
            Assert.That(result.Single(c => c.Name == "Weiz").GetEdges.Count, Is.EqualTo(4));
        });
    }
}