namespace DataStructure.BinaryHeap
{
    public class BinaryHeap<T> : IBinaryHeap<T>
        where T : IComparable<T>
    {
        private int _size = 128;
        private T[] _items;
        private int _currentIndex = -1;

        public BinaryHeap()
        {
            _items = new T[_size];
        }

        public BinaryHeap(IEnumerable<T> items)
            : this()
        {
            foreach (T item in items)
                Insert(item);
        }

        public int Length => _items.Length;
        public int Count => _currentIndex + 1;

        public void Insert(T value)
        {
            if (Count == Length)
            {
                Resize();
            }
            var currentIndex = ++_currentIndex;
            _items[currentIndex] = value;

            var parrentIndex = GetParrentIndex(currentIndex);

            while (parrentIndex != currentIndex)
            {
                var parentValue = _items[parrentIndex];
                if (parentValue.CompareTo(value) < 0)
                {
                    _items[parrentIndex] = value;
                    _items[currentIndex] = parentValue;
                }
                currentIndex = parrentIndex;
                parrentIndex = GetParrentIndex(currentIndex);
            }
        }

        private void Resize(int? count = null)
        {
            var size = count == null ? _size * 2 : count * 2;
            _size = size.Value;

            Array.Resize(ref _items, _size);
        }

        public T? Max()
        {
            return Length > 0 ? _items[0] : default;
        }

        public T? PopMax()
        {
            var currentMax = Max();

            ref var lastInserted = ref _items[_currentIndex];
            _items[0] = lastInserted;   
            --_currentIndex;
            var rootIndex = 0;
            while (rootIndex * 2 + 1 < _currentIndex)
            {
                ref var currentValue = ref _items[rootIndex];
                var leftChildIndex = GetLeftChildIndex(rootIndex);
                var rightChildIndex = GetRightChildIndex(rootIndex);
                ref var leftChild = ref _items[leftChildIndex];
                ref var rightChild = ref _items[rightChildIndex];

                if (currentValue.CompareTo(leftChild) < 0 && rightChild.CompareTo(leftChild) < 0)
                {
                    Swap(rootIndex, ref currentValue, leftChildIndex, ref leftChild);
                    rootIndex = leftChildIndex;
                }
                else if (currentValue.CompareTo(rightChild) < 0 && leftChild.CompareTo(rightChild) < 0)
                {
                    Swap(rootIndex, ref currentValue, rightChildIndex, ref rightChild);
                    rootIndex = rightChildIndex;
                }
                else
                {
                    break;
                }
            }

            return currentMax;
        }

        private void Swap(int rootIndex, ref T currentValue, int leftChildIndex, ref T leftChild)
        {
            _items[rootIndex] = leftChild;
            _items[leftChildIndex] = currentValue;
        }

        private static int GetParrentIndex(int currentIndex)
        {
            return (currentIndex - 1) / 2;
        }

        private static int GetLeftChildIndex(int parentIndex) => 2 * parentIndex + 1;
        private static int GetRightChildIndex(int parentIndex) => 2 * parentIndex + 2;
    }
}
