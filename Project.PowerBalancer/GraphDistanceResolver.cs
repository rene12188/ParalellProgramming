using Project.Util.Models.Import;

namespace Project.PowerBalancer;

public class GraphDistanceResolver
{
    private Dictionary<string, Dictionary<string, double>> _adjacencyMatrix = new();

    private Dictionary<string, Community> _communities = new();

    public GraphDistanceResolver(IList<ImportCommunity> importCommunities)
    {
        foreach (var importCommunity in importCommunities)
        {
            var valueList = importCommunities.ToDictionary(k => k.Name, v => v.Edges.FirstOrDefault(c => c.To == importCommunity.Name)?.Cost ?? 100000);
            _adjacencyMatrix.Add(importCommunity.Name, valueList);
        }

        FloydWarshall();
    }

    public void FloydWarshall()
    {
        // loop through all pairs of nodes and update the shortest path between them
        Parallel.ForEach(_adjacencyMatrix.Keys, k =>
        {
            foreach (string i in _adjacencyMatrix.Keys)
            foreach (string j in _adjacencyMatrix.Keys)
                if (_adjacencyMatrix[i].ContainsKey(k) && _adjacencyMatrix[k].ContainsKey(j) &&
                    (!_adjacencyMatrix[i].ContainsKey(j) || _adjacencyMatrix[i][j] > _adjacencyMatrix[i][k] + _adjacencyMatrix[k][j]))
                    _adjacencyMatrix[i][j] = _adjacencyMatrix[i][k] + _adjacencyMatrix[k][j];
        });
    }

    public Dictionary<string, double> GetDistancesFromSource(string from)
    {
        if (_adjacencyMatrix.TryGetValue(from, out var value))
        {
            var dict = new Dictionary<string, double>(value);
            dict.Remove(from);
            return dict;
        }

        throw new ArgumentException("No Node Found");
    }

    public void SetCommunities(IList<Community> communities)
    {
        _communities = communities.ToDictionary(c => c.Name, c => c);
    }

    public Community? GetCommunity(string name)
    {
        if (_communities.TryGetValue(name, out var community))
            return community;
        return null;
    }
}