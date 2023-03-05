// See https://aka.ms/new-console-template for more information


using Newtonsoft.Json;
using Project.PowerBalancer;
using Project.Util.Models.Import;

var data = File.ReadAllText("./communities.json");

var communities = JsonConvert.DeserializeObject<List<ImportCommunity>>(data);
var clock = new Clock(int.Parse(args[0]));
var powerBalancerEngine = new PowerBalancerEngine(communities!, new PowerSystemConfig(clock), clock);
powerBalancerEngine.Start();

Console.WriteLine("Hello, World!");