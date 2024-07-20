namespace DataStructure.BinaryHeap.Tests
{
    public class Tests
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

            Assert.That(binaryHeap.Length, Is.EqualTo(7));
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
    }
}