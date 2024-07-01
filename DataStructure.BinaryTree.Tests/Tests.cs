namespace DataStructure.BinaryTree.Tests
{
    public class Tests
    {

        [Test]
        public void CreateBinaryTree_ShouldCreate()
        {
            var list = new List<int>() { -5, 0, 2, 2, 3, 5 };
            var binaryTree = new BinaryTree<int>();
            list.ForEach(binaryTree.Insert);

            var resultNodes = new List<TreeNode<int>>();

            void getChildrenNode(TreeNode<int> parentNode)
            {
                if (parentNode is null)
                    return;
                resultNodes.Add(parentNode);
                var childNodes = parentNode.GetChildrens().ToList();
                childNodes.ForEach(node => getChildrenNode(node));
            }

            resultNodes.Add(binaryTree.Root);

            foreach (var childNode in binaryTree.Root.GetChildrens())
            {
                getChildrenNode(childNode);
            }

            Assert.That(list.Distinct().Count, Is.EqualTo(resultNodes.Count));
        }

        [Test]
        public void SearchBinaryTree_ShouldReturnValue()
        {
            var list = new List<int>() { -5, 0, 2, 2, 3, 5 };
            var binaryTree = new BinaryTree<int>();
            list.ForEach(binaryTree.Insert);

            var result = binaryTree.Find(3);

            Assert.IsNotNull(result);
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void SearchBinaryTree_ShouldReturnNull()
        {
            var list = new List<int>() { -5, 0, 2, 2, 3, 5 };
            var binaryTree = new BinaryTree<int>();
            list.ForEach(binaryTree.Insert);

            Assert.Throws<InvalidOperationException>(() => binaryTree.Find(10));
        }

        [Test]
        public void BinaryTree_ShouldReturnAllElements()
        {
            var list = new List<int>() { -5, 0, 2, 2, 3, 5 };
            var binaryTree = new BinaryTree<int>();
            list.ForEach(binaryTree.Insert);

            var result = binaryTree.InWidth().ToArray();

            Assert.That(result, Is.EqualTo(list.Distinct()));
        }

        [Test]
        public void BinaryTree_ShouldReturnSortElements()
        {
            var list = new List<int>() { -5, -6, 0, 2, 2, 3, 5 };
            var binaryTree = new BinaryTree<int>();
            list.ForEach(binaryTree.Insert);

            var result = binaryTree.SortTree().ToArray();

            list.Sort();

            Assert.That(result, Is.EqualTo(list.Distinct()));
        }

    }
}