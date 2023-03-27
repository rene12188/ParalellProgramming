namespace Project.PowerBalancer.Interfaces;

public interface IReporter
{
    void Report(IList<Community> communities);
    void FlushReport(IList<Community> communities);
}