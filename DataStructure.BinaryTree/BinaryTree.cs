using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure.BinaryTree
{
    public class BinaryTree<T>
        where T :IComparable<T>

    {
        public TreeNode<T> Root { get; private set; }

        public BinaryTree()
        {
            
        }

        public BinaryTree(T value)
        {
            Root = new TreeNode<T>(value);
        }

        public BinaryTree(IEnumerable<T> values)
        {
            foreach (var value in values)
                Insert(value);
        }

        public void Insert(T value)
        {
            if (Root is null)
            {
                Root = new TreeNode<T>(value);
            }
            else
            {
                Root.Insert(value);
            }

        }

        public T Find(T value)
        {
            var node = Root;
            return Find(value, node);
        }

        private T Find(T value, TreeNode<T> node)
        {
            while (node != null)
            {
                if (node.Value.CompareTo(value) == 0) return node.Value;

                node = value.CompareTo(node.Value) < 0 ? node.Left : node.Right;
            }

            throw new InvalidOperationException();
        }

        public IEnumerable<T> SortTree()
        {
            return SortNode(Root);
        }

        private IEnumerable<T> SortNode(TreeNode<T> node)
        {
            if (node.Left is not null)
            {
                var sortNodes = SortNode(node.Left);

                foreach (var leftNode in sortNodes)
                    yield return leftNode;
            }

            yield return node.Value;

            if (node.Right is not null)
            {
                var sortedNodes = SortNode(node.Right);
                foreach (var rightNode in sortedNodes)
                    yield return rightNode;
            }

        }
    }
}
