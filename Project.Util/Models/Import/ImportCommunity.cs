using System.Collections.ObjectModel;

namespace Project.Util.Models.Import;

public class ImportCommunity
{
    public string Name { get; private set; }

    private readonly List<ImportEdge> _edges = new();

    public ReadOnlyCollection<ImportEdge> GetEdges => _edges.AsReadOnly();

    public ImportCommunity(string name)
    {
        Name = name;
    }

    public void AddEdge(ImportEdge edge)
    {
        CheckConstraintsForEdges(edge);

        _edges.Add(edge);
    }

    private void CheckConstraintsForEdges(ImportEdge edge)
    {
        if (edge.From != Name) throw new ArgumentException("Edge does not belong to this community");

        if (_edges.Any(c => c.To == edge.To)) throw new ArgumentException("Edge already exists");
    }
}