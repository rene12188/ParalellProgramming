// See https://aka.ms/new-console-template for more information

using Newtonsoft.Json;
using Project.DataGenerator;
using Project.Util.DataGenerator;
using Project.Util.Models.Import;

double GetPseudoDoubleWithinRange(double lowerBound, double upperBound)
{
    var random = new Random();
    var rDouble = random.NextDouble();
    var rRangeDouble = rDouble * (upperBound - lowerBound) + lowerBound;
    return rRangeDouble;
}

void GenerateEdges(int i1, ImportCommunity importCommunity)
{
    for (int j = 0; j < 10; j++)
        if (i1 - j != i1)
            if (i1 - j >= 0)
                importCommunity.Edges.Add(new ImportEdge($"C{i1}", $"C{i1 - j}", GetPseudoDoubleWithinRange(0.1, 5.0)));
}

IList<ImportCommunity> CreateDenseCommunities()
{
    var communities = new List<ImportCommunity>();
    for (int i = 0; i < 1000; i++)
    {
        var newCommunity = new ImportCommunity($"C{i}");
        GenerateEdges(i, newCommunity);

        communities.Add(newCommunity);
    }

    var external = new ImportCommunity("External");
    external.AddEdge(new ImportEdge(external.Name, "C0", 50));
    communities.Add(external);
    return communities;
}

var dataGenerator = new ImportCommunityFactory();

DataGeneratorConfig.CreateConfig(dataGenerator);


var jsonRealString = JsonConvert.SerializeObject(dataGenerator.GetCommunities());
var jsonFictitiousString = JsonConvert.SerializeObject(CreateDenseCommunities());

File.WriteAllText("../../../../Project.ConsoleApplication/communities.json", jsonRealString);
File.WriteAllText("../../../../Project.ConsoleApplication/communitiesFictitious.json", jsonFictitiousString);