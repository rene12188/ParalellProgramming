using BenchmarkDotNet.Running;
using ParallelSorting;

BenchmarkRunner.Run<QuickSortBenchmarks>();
BenchmarkRunner.Run<MergeSortBenchmarks>();