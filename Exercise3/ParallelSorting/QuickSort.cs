// sorting logic mostly copied from: https://www.w3resource.com/csharp-exercises/searching-and-sorting-algorithm/searching-and-sorting-algorithm-exercise-9.php

internal static class QuickSort
{
    public static void RunSequential<T>(IList<T> arr) where T : IComparable<T>
    {
        InternalRunSequential(arr, 0, arr.Count - 1);
    }

    public static async Task RunParallel<T>(IList<T> arr) where T : IComparable<T>
    {
        await InternalRunParallel(arr, 0, arr.Count - 1);
    }

    public static async Task RunParallelWithLimitedThreads<T>(IList<T> arr, int threadCount) where T : IComparable<T>
    {
        await InternalRunParallelWithLimitedThreads(arr, 0, arr.Count - 1, threadCount);
    }

    public static async Task RunParallelWithThreshold<T>(IList<T> arr, int threshold) where T : IComparable<T>
    {
        await InternalRunParallelWithThreshold(arr, 0, arr.Count - 1, threshold);
    }

    private static async Task InternalRunParallelWithThreshold<T>(IList<T> arr, int left, int right, int threshold) where T : IComparable<T>
    {
        if (left < right)
        {
            int rangeToSort = right - left;
            int pivot = rangeToSort >= threshold ? await ParallelPartition(arr, left, right) : SequentialPartition(arr, left, right);
            Task task1 = Task.CompletedTask;
            Task task2 = Task.CompletedTask;

            if (pivot > 1)
            {
                if (rangeToSort >= threshold)
                {
                    task1 = InternalRunParallelWithThreshold(arr, left, pivot - 1, threshold);
                }
                else
                {
                    InternalRunSequential(arr, left, pivot - 1);
                }
            }

            if (pivot + 1 < right)
            {
                if (rangeToSort >= threshold)
                {
                    task2 = InternalRunParallelWithThreshold(arr, pivot + 1, right, threshold);
                }
                else
                {
                    InternalRunSequential(arr, pivot + 1, right);
                }
            }

            Task.WaitAll(task1, task2);
        }
    }

    private static async Task InternalRunParallelWithLimitedThreads<T>(IList<T> arr, int left, int right, int threadCount) where T : IComparable<T>
    {
        if (left < right)
        {
            int pivot = SequentialPartition(arr, left, right);
            Task task1 = Task.CompletedTask;
            Task task2 = Task.CompletedTask;

            if (pivot > 1)
            {
                if (threadCount > 0)
                {
                    task1 = InternalRunParallelWithLimitedThreads(arr, left, pivot - 1, threadCount - 1);
                }
                else
                {
                    InternalRunSequential(arr, left, pivot - 1);
                }
            }

            if (pivot + 1 < right)
            {
                if (threadCount > 0)
                {
                    task2 = InternalRunParallelWithLimitedThreads(arr, pivot + 1, right, threadCount - 1);
                }
                else
                {
                    InternalRunSequential(arr, pivot + 1, right);
                }
            }

            Task.WaitAll(task1, task2);
        }
    }

    private static async Task InternalRunSequentialWithParallelPartition<T>(IList<T> arr, int left, int right) where T : IComparable<T>
    {
        if (left < right)
        {
            int pivot = await ParallelPartition(arr, left, right);

            if (pivot > 1)
            {
                InternalRunSequential(arr, left, pivot - 1);
            }

            if (pivot + 1 < right)
            {
                InternalRunSequential(arr, pivot + 1, right);
            }
        }
    }

    private static async Task InternalRunParallel<T>(IList<T> arr, int left, int right) where T : IComparable<T>
    {
        if (left < right)
        {
            int pivot = SequentialPartition(arr, left, right);
            Task task1 = Task.CompletedTask;
            Task task2 = Task.CompletedTask;
            
            if (pivot > 1)
            {
                task1 = InternalRunParallel(arr, left, pivot - 1);
            }

            if (pivot + 1 < right)
            {
                task2 = InternalRunParallel(arr, pivot + 1, right);
            }

            Task.WaitAll(task1, task2);
        }
    }

    private static void InternalRunSequential<T>(IList<T> arr, int left, int right) where T : IComparable<T>
    {
        if (left < right)
        {
            int pivot = SequentialPartition(arr, left, right);

            if (pivot > 1)
            {
                InternalRunSequential(arr, left, pivot - 1);
            }
            if (pivot + 1 < right)
            {
                InternalRunSequential(arr, pivot + 1, right);
            }
        }
    }

    private async static Task<int> ParallelPartition<T>(IList<T> arr, int left, int right) where T : IComparable<T>
    {
        T pivot = arr[left];
        while (true)
        {
            var task1 = Task.Run(() => { while (arr[left].CompareTo(pivot) < 0) { left++; } } );
            var task2 = Task.Run(() => { while (arr[right].CompareTo(pivot) > 0) { right--; } });

            Task.WaitAll(task1, task2);

            if (left < right)
            {
                if (arr[left].CompareTo(arr[right]) == 0) return right;

                (arr[left], arr[right]) = (arr[right], arr[left]);
            }
            else
            {
                return right;
            }
        }
    }

    private static int SequentialPartition<T>(IList<T> arr, int left, int right) where T: IComparable<T>
    {
        T pivot = arr[left];
        while (true)
        {
            while (arr[left].CompareTo(pivot) < 0) { left++; }
            while (arr[right].CompareTo(pivot) > 0) { right--; }

            if (left < right)
            {
                if (arr[left].CompareTo(arr[right]) == 0) return right;

                (arr[left], arr[right]) = (arr[right], arr[left]);
            }
            else
            {
                return right;
            }
        }
    }
}

