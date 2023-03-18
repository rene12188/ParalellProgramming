using BenchmarkDotNet.Attributes;

namespace ParallelSorting
{
    [MemoryDiagnoser]
    public class MergeSortBenchmarks
    {
        // always have the same numbers
        [Params(1000000)]
        public int ElementsCount { get; set; }

        [Params(10000, 25000, 50000, 75000, 100000)]
        public int Threshold { get; set; }
        public IList<int> Numbers => Enumerable.Range(0, ElementsCount).Select(_ => Random.Shared.Next(0, int.MaxValue)).ToArray();


        [Benchmark]
        public void MergeSortSequential()
        {
            MergeSort.RunSequential(Numbers);
        }

        [Benchmark]
        public async Task MergeSortWithLimitedParallelism()
        {
            await MergeSort.RunParallelWithThreshold(Numbers, Threshold);
        }

        [Benchmark]
        public async Task MergeSortParallel()
        {
            await MergeSort.RunParallel(Numbers);
        }

    }
}
