namespace Project.PowerBalancer.Interfaces;

public interface IReporter
{
    void Report(IList<Community> communities);
}