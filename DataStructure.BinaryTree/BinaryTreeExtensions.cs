using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure.BinaryTree
{
    public static class BinaryTreeExtensions
    {
        public static IEnumerable<T> InWidth<T>(this BinaryTree<T> tree)
            where T : IComparable<T>
        {
            var nodes = tree.GetChildnres();

            var queue = new Queue<ITreeNode<T>>();
            foreach ( var node in nodes ) { queue.Enqueue(node); }

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                yield return node.Value;

                foreach(var childNote in node.GetChildnres())
                {
                    if (childNote != null)
                        queue.Enqueue(childNote);
                }
            }

        }
    }
}
