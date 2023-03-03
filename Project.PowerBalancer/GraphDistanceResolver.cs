using Project.Util.Models.Import;

namespace Project.PowerBalancer;

public class GraphDistanceResolver
{
    private Dictionary<string, Dictionary<string, double>> _adjacencyMatrix = new();

    public GraphDistanceResolver(IList<ImportCommunity> importCommunities)
    {
        foreach (var importCommunity in importCommunities)
        {
            var valueList = importCommunities.ToDictionary(k => k.Name, v => v.GetEdges.FirstOrDefault(c => c.To == importCommunity.Name)?.Cost ?? double.MaxValue);
            _adjacencyMatrix.Add(importCommunity.Name, valueList);
        }

        FloydWarshall();
    }

    public void FloydWarshall()
    {
        // loop through all pairs of nodes and update the shortest path between them
        foreach (string k in _adjacencyMatrix.Keys)
        foreach (string i in _adjacencyMatrix.Keys)
        foreach (string j in _adjacencyMatrix.Keys)
            if (_adjacencyMatrix[i].ContainsKey(k) && _adjacencyMatrix[k].ContainsKey(j) &&
                (!_adjacencyMatrix[i].ContainsKey(j) || _adjacencyMatrix[i][j] > _adjacencyMatrix[i][k] + _adjacencyMatrix[k][j]))
                _adjacencyMatrix[i][j] = _adjacencyMatrix[i][k] + _adjacencyMatrix[k][j];
    }

    public double GetDistance(string fron, string to)
    {
        if (_adjacencyMatrix.TryGetValue(fron, out var value))
            if (value.TryGetValue(to, out var result))
                return result;

        throw new ArgumentException("No  Node Found");
    }
}