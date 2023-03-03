namespace Project.DataGenerator.Tests;

public class ManualTesting
{
    [Test]
    public void Test1()
    {
        Parallel.For(0, 100, i => { Console.WriteLine(i); });
    }
}