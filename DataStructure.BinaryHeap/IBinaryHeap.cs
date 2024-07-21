namespace DataStructure.BinaryHeap
{
    public interface IBinaryHeap<T>
        where T : IComparable<T>
    {
        T? Max();

        T? PopMax();

        void Insert(T value);

        int Length { get; }

        int Count { get; }
    }
}
