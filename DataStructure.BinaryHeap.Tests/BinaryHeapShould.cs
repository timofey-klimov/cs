namespace DataStructure.BinaryHeap.Tests
{
    public class BinaryHeapShould
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BinaryHeap_ShouldInsert()
        {
            var binaryHeap = new BinaryHeap<int>();
            binaryHeap.Insert(11);
            binaryHeap.Insert(17);
            binaryHeap.Insert(20);
            binaryHeap.Insert(7);
            binaryHeap.Insert(4);
            binaryHeap.Insert(13);
            binaryHeap.Insert(15);

            Assert.That(binaryHeap.Count, Is.EqualTo(7));
        }

        [Test]
        public void BinaryHeap_ShouldReturnMax()
        {
            var binaryHeap = new BinaryHeap<int>();
            binaryHeap.Insert(11);
            binaryHeap.Insert(17);
            binaryHeap.Insert(20);
            binaryHeap.Insert(7);
            binaryHeap.Insert(4);
            binaryHeap.Insert(13);
            binaryHeap.Insert(15);

            var max = binaryHeap.Max();

            Assert.That(max, Is.EqualTo(20));
        }

        [Test]
        public void BinaryHeap_ShouldPopMax()
        {
            var binaryHeap = new BinaryHeap<int>();
            binaryHeap.Insert(11);
            binaryHeap.Insert(17);
            binaryHeap.Insert(20);
            binaryHeap.Insert(7);
            binaryHeap.Insert(4);
            binaryHeap.Insert(13);
            binaryHeap.Insert(15);

            binaryHeap.PopMax();
            binaryHeap.PopMax();
            var max = binaryHeap.Max();

            Assert.That(max, Is.EqualTo(15));
        }

        [Test]
        public void BinaryHeap_ShouldCreateWithConstructor()
        {
            var binaryHeap = new BinaryHeap<int>(new List<int> { 11, 17, 20, 7, 4, 13, 15, 36});

            Assert.That(binaryHeap.Max(), Is.EqualTo(36));
        }

        [Test]
        public void BinaryHeap_ShouldoPop()
        {
            var heap = new BinaryHeap<int>();

            for (int i = 0; i < 10_000_000; i++)
            {
                heap.Insert(i);
            }

            int max = 0;

            for (int i = 0; i < 10_000_000; i++)
            {
                max = heap.PopMax();
            }

            Assert.That(max, Is.EqualTo(0));
        }
    }
}