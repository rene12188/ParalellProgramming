using Perfolizer.Mathematics.Thresholds;

namespace ParallelSorting;

// implementation: https://code-maze.com/csharp-merge-sort/
internal class MergeSort
{
    public static IList<T> RunSequential<T>(IList<T> list) where T : IComparable
    {
        return InternalSequentialSort(list, 0, list.Count - 1);
    }

    public static async Task<IList<T>> RunParallel<T>(IList<T> list) where T : IComparable
    {
        return await InternalParallelSort(list, 0, list.Count - 1);
    }

    public static async Task<IList<T>> RunParallelWithThreshold<T>(IList<T> list, int threshold) where T : IComparable
    {
        return await InternalParallelSortWithThreshold(list, 0, list.Count - 1, threshold);
    }

    private static async Task<IList<T>> InternalParallelSortWithThreshold<T>(IList<T> array, int left, int right, int threshold) where T : IComparable
    {
        if (left < right)
        {
            int rangeToSort = right - left;
            Task task1 = Task.CompletedTask;
            Task task2 = Task.CompletedTask;

            int middle = left + (right - left) / 2;

            if (rangeToSort >= threshold)
            {
                task1 = InternalParallelSortWithThreshold(array, left, middle, threshold);
                task2 = InternalParallelSortWithThreshold(array, middle + 1, right, threshold);
            }
            else
            {
                InternalSequentialSort(array, left, middle);
                InternalSequentialSort(array, middle + 1, right);
            }

            Task.WaitAll(task1, task2);

            //InternalParallelMerge(array, left, middle, right);
            InternalSequentialMerge(array, left, middle, right);
        }

        return array;
    }

    private static async Task<IList<T>> InternalParallelSort<T>(IList<T> array, int left, int right) where T : IComparable
    {
        if (left < right)
        {
            int middle = left + (right - left) / 2;
            var task1 = InternalParallelSort(array, left, middle);
            var task2 = InternalParallelSort(array, middle + 1, right);
            Task.WaitAll(task1, task2);
            InternalSequentialMerge(array, left, middle, right);
        }

        return array;
    }

    private static IList<T> InternalSequentialSort<T>(IList<T> array, int left, int right) where T : IComparable
    {
        if (left < right)
        {
            int middle = left + (right - left) / 2;
            InternalSequentialSort(array, left, middle);
            InternalSequentialSort(array, middle + 1, right);
            InternalSequentialMerge(array, left, middle, right);
        }

        return array;
    }

    private static void InternalSequentialMerge<T>(IList<T> array, int left, int middle, int right) where T : IComparable
    {
        var leftArrayLength = middle - left + 1;
        var rightArrayLength = right - middle;
        var leftTempArray = new T[leftArrayLength];
        var rightTempArray = new T[rightArrayLength];
        int i, j;

        for (i = 0; i < leftArrayLength; ++i)
            leftTempArray[i] = array[left + i];
        for (j = 0; j < rightArrayLength; ++j)
            rightTempArray[j] = array[middle + 1 + j];

        i = 0;
        j = 0;
        int k = left;

        while (i < leftArrayLength && j < rightArrayLength)
        {
            if (leftTempArray[i].CompareTo(rightTempArray[j]) <= 0)
            {
                array[k++] = leftTempArray[i++];
            }
            else
            {
                array[k++] = rightTempArray[j++];
            }
        }

        while (i < leftArrayLength)
        {
            array[k++] = leftTempArray[i++];
        }

        while (j < rightArrayLength)
        {
            array[k++] = rightTempArray[j++];
        }
    }

    private static void InternalParallelMerge<T>(IList<T> array, int left, int middle, int right) where T : IComparable
    {
        var leftArrayLength = middle - left + 1;
        var rightArrayLength = right - middle;
  
        int i, j;

        var task1 = Task.Run(() =>
        {
            var leftTempArray = new T[leftArrayLength];
            for (i = 0; i < leftArrayLength; ++i) leftTempArray[i] = array[left + i];
            return leftTempArray;
        });

        var task2 = Task.Run(() =>
        {
            var rightTempArray = new T[rightArrayLength];
            for (j = 0; j < rightArrayLength; ++j) rightTempArray[j] = array[middle + 1 + j];
            return rightTempArray;
        });

        Task.WaitAll(task1, task2);
        var leftTempArray = task1.Result;
        var rightTempArray = task2.Result;

        i = 0;
        j = 0;
        int k = left;

        while (i < leftArrayLength && j < rightArrayLength)
        {
            if (leftTempArray[i].CompareTo(rightTempArray[j]) <= 0)
            {
                array[k++] = leftTempArray[i++];
            }
            else
            {
                array[k++] = rightTempArray[j++];
            }
        }

        while (i < leftArrayLength)
        {
            array[k++] = leftTempArray[i++];
        }

        while (j < rightArrayLength)
        {
            array[k++] = rightTempArray[j++];
        }
    }
}

