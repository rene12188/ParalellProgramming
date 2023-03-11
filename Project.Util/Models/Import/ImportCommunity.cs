using System.Collections.ObjectModel;

namespace Project.Util.Models.Import;

public class ImportCommunity
{
    public string Name { get; private set; }

    public readonly List<ImportEdge> Edges = new();
    

    public ImportCommunity(string name)
    {
        Name = name;
    }

    public void AddEdge(ImportEdge edge)
    {
        CheckConstraintsForEdges(edge);
        Edges.Add(edge);
    }

    private void CheckConstraintsForEdges(ImportEdge edge)
    {
        if(edge.To == edge.From) throw new ArgumentException("Edge cannot be from and to the same community");
        if (edge.From != Name) throw new ArgumentException("Edge does not belong to this community");

        if (Edges.Any(c => c.To == edge.To)) throw new ArgumentException("Edge already exists");
    }
}