namespace DataStructure.BinaryTree
{
    public interface ITreeNode<T>
    {
        public T Value { get; }
        IEnumerable<ITreeNode<T>> GetChildnres();
    }

    public class TreeNode<T> : ITreeNode<T>
        where T : IComparable<T>
    {
        public TreeNode<T> Left { get; private set; }

        public TreeNode<T> Right { get; private set; }

        public T Value { get; private set; }

        public IEnumerable<ITreeNode<T>> GetChildnres()
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
                    node.Left = new TreeNode<T>(value);
                else
                    Insert(node.Left, value);
            }

            if (value.CompareTo(node.Value) > 0)
            {
                if (node.Right == null)
                    node.Right = new TreeNode<T>(value);
                else
                    Insert(node.Right, value);
            }
        }
    }
}
