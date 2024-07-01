namespace DataStructure.BinaryTree
{
    public class TreeNode<T>
        where T : IComparable<T>
    {
        private TreeNode<T> _left;
        private TreeNode<T> _right;
        public TreeNode<T> Left => _left;

        public TreeNode<T> Right => _right;

        public T Value { get; private set; }

        public IEnumerable<TreeNode<T>> GetChildrens()
        {
            yield return Left;
            yield return Right;
        }

        public TreeNode(T value)
        {
            Value = value;
        }

        public void Insert(T value)
        {
            Insert(this, value);
        }

        private void Insert(TreeNode<T> node, T value)
        {
            if (value.CompareTo(node.Value) < 0)
            {
                if (node.Left == null)
                    node._left = new TreeNode<T>(value);
                else
                    Insert(node.Left, value);
            }

            if (value.CompareTo(node.Value) > 0)
            {
                if (node.Right == null)
                    node._right = new TreeNode<T>(value);
                else
                    Insert(node.Right, value);
            }
        }
    }
}
