namespace DataStructure.BorTree.Tests
{
    public class PrefixTreeShould
    {
        [SetUp]
        public void Setup()
        {
        }

        /// </summary>
        [Test]
        public void PrefixTree_Retrieve()
        {
            var tree = new PrefixTree();
            tree.Insert("Mean", 100);
            tree.Insert("Meaning", 200);
            tree.Insert("Meaningful", 300);
            tree.Insert("Meandering", 100);
            tree.Insert("Meaningless");
            tree.Insert("Meanwhile");
            tree.Insert("Meanie");

            var keys = tree.RetrieveKeys("Mean").ToList();

            Assert.That(keys.Count, Is.EqualTo(7));
        }

    }
}