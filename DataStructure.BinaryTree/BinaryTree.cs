using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure.BinaryTree
{
    public class BinaryTree<T> : ITreeNode<T>
        where T :IComparable<T>

    {
        private TreeNode<T> Root { get; set; }

        public T Value => throw new NotImplementedException();

        public BinaryTree()
        {
            
        }

        public BinaryTree(T value)
        {
            Root = new TreeNode<T>(value);
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

        public IEnumerable<ITreeNode<T>> GetChildnres()
        {
            yield return Root;
        }
    }
}
