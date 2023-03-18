using System.Runtime.InteropServices.JavaScript;
using BenchmarkDotNet.Attributes;

namespace ParallelSorting
{
    [MemoryDiagnoser]
    public class QuickSortBenchmarks
    {
        [Params(1000000)]
        public int ElementsCount { get; set; }

        [Params(10000, 25000, 50000, 75000, 100000)]
        public int Threshold { get; set; }

        public IList<int> Numbers => Enumerable.Range(0, ElementsCount).Select(_ => Random.Shared.Next(0, int.MaxValue)).ToArray();

        [Benchmark]
        public void QuickSortSequential()
        {
            QuickSort.RunSequential(Numbers);
        }

        [Benchmark]
        public async Task QuickSortParallel()
        {
            await QuickSort.RunParallel(Numbers);
        }

        [Benchmark]
        public async Task QuickSortParallelWithThreshold()
        {
            await QuickSort.RunParallelWithThreshold(Numbers, Threshold);
        }
    }
}
