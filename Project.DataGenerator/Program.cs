// See https://aka.ms/new-console-template for more information

using Newtonsoft.Json;
using Project.DataGenerator;
using Project.Util.DataGenerator;

var dataGenerator = new ImportCommunityFactory();

DataGeneratorConfig.CreateConfig(dataGenerator);

dataGenerator.GetCommunities();

var jsonString = JsonConvert.SerializeObject(dataGenerator.GetCommunities());
File.WriteAllText("../../../../Project.ConsoleApplication/communities.json", jsonString);