namespace DataStructure.HashSet.Tests
{
    public class HashSetShould
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void HashSet_ShouldContains()
        {
            var hashSet = new HashSet<int>();
            for (int i = 0; i < 15; i++)
            {
                hashSet.Add(i);
            }

            for (int i = 0;i < 15; i++)
            {
                var contains = hashSet.Contains(i);
                Assert.IsTrue(contains);
            }
        }
    }
}