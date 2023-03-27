// See https://aka.ms/new-console-template for more information


using Newtonsoft.Json;
using Project.PowerBalancer;
using Project.PowerBalancer.Modules.Clocks;
using Project.PowerBalancer.Modules.PowerSystemConfig;
using Project.Util.Models.Import;
using Serilog;

long StartBalancingConcurrent(string s)
{
    var communities = JsonConvert.DeserializeObject<List<ImportCommunity>>(s).OrderBy(s => s.Name).ToList();
    var clock = new WaitingClock(new PowerBalancingMediator(), 40000, 40010);
    var powerBalancerEngine1 = new PowerBalancerEngine(communities, new FictitiousPowerSystemConfig(clock), clock);
    return powerBalancerEngine1.StartConcurrent();
}

long StartBalancingSequential(string s)
{
    var communities = JsonConvert.DeserializeObject<List<ImportCommunity>>(s);
    var clock = new WaitingClock(new PowerBalancingMediator(), 40000, 40010);
    var powerBalancerEngine1 = new PowerBalancerEngine(communities!, new FictitiousPowerSystemConfig(clock), clock);
    return powerBalancerEngine1.StartSequential();
}

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
var data = File.ReadAllText("./communitiesFictitious.json");

var concurrent = StartBalancingConcurrent(data);
var sequential = StartBalancingSequential(data);
Log.Warning($"Sequential: {sequential}ms vs Concurrent: {concurrent}ms");