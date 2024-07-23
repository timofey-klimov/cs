using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using DataStructure.BinaryHeap;
using System.Text;

BenchmarkRunner.Run<BinaryHeapBench>();
[MemoryDiagnoser]
public class BinaryHeapBench
{
//| Method                | Mean     | Error   | StdDev  | Gen0      | Gen1      | Gen2      | Allocated |
//|---------------------- |---------:|--------:|--------:|----------:|----------:|----------:|----------:|
//| BinaryHeapWithoutRefs | 203.1 ms | 4.01 ms | 4.92 ms | 3666.6667 | 3666.6667 | 3666.6667 |    128 MB |
    [Benchmark]
    public void BinaryHeapWithoutRefs()
    {
        var heap = new BinaryHeap<int>();

        for (int i = 0; i < 10_000_000; i++)
        {
            heap.Insert(i);
        }
    }
//WithoutRefs
//    | Method                | Mean     | Error   | StdDev  | Gen0      | Gen1      | Gen2      | Allocated |
//|---------------------- |---------:|--------:|--------:|----------:|----------:|----------:|----------:|
//| BinaryHeapWithoutRefs | 194.0 ms | 0.34 ms | 0.26 ms | 3666.6667 | 3666.6667 | 3666.6667 |    128 MB |
//| BinaryHeapPopMax      | 482.5 ms | 1.68 ms | 1.57 ms | 3000.0000 | 3000.0000 | 3000.0000 |    128 MB |

//WithRefs
//| Method                | Mean     | Error   | StdDev  | Gen0      | Gen1      | Gen2      | Allocated |
//|---------------------- |---------:|--------:|--------:|----------:|----------:|----------:|----------:|
//| BinaryHeapWithoutRefs | 198.0 ms | 1.41 ms | 1.32 ms | 3666.6667 | 3666.6667 | 3666.6667 |    128 MB |
//| BinaryHeapPopMax      | 223.1 ms | 1.25 ms | 1.17 ms | 3666.6667 | 3666.6667 | 3666.6667 |    128 MB |
    [Benchmark]
    public void BinaryHeapPopMax()
    {
        var heap = new BinaryHeap<int>();

        for (int i = 0; i < 10_000_000; i++)
        {
            heap.Insert(i);
        }

        for (int i = 0; i < heap.Count; i++)
        {
            heap.PopMax();
        }
    }
}
