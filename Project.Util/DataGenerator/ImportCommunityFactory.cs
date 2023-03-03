using Project.Util.Models.Import;

namespace Project.DataGenerator;

public class ImportCommunityFactory
{
    private readonly List<ImportCommunity> _communities = new();

    public void AddCommunity(string name)
    {
        if (_communities.Any(c => c.Name == name)) throw new ArgumentException("Community already exists");

        _communities.Add(new ImportCommunity(name));
    }

    public void AddEdge(string from, string to, double cost)
    {
        var fromCommunity = _communities.Single(c => c.Name == from);
        var toCommunity = _communities.Single(c => c.Name == to);

        fromCommunity.AddEdge(new ImportEdge(from, to, cost));
        toCommunity.AddEdge(new ImportEdge(to, from, cost));
    }

    public IList<ImportCommunity> GetCommunities()
    {
        return _communities.ToList();
    }
}