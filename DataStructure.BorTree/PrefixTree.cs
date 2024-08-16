using System.Diagnostics;
using System.Text;

namespace DataStructure.BorTree
{
    public class PrefixTree
    {

        public PrefixTree(string value, object? info = null) 
        {
            Insert(value, info);
        }

        public PrefixTree()
        {
            
        }
        public HashSet<TreeNode> Children { get; init; } = new HashSet<TreeNode>();

        public void Insert(string word, object? info = null)
        {
            var children = Children;
            word = word.Trim();
            for (int i = 0; i < word.Length; i++)
            {
                if (children.TryGetValue(word[i], out var childNode))
                {
                    if (word.Length - 1 == i)
                        childNode.SetKey(info);
                    children = childNode.Children;
                }
                else
                {
                    var node = new TreeNode(word[i], word.Length - 1 == i ? info : null , word.Length - 1 == i);
                    children.Add(node);
                    children = node.Children;
                }
            }
        }

        public IEnumerable<(string, object?)> RetrieveKeys(string prefix)
        {
            var children = Children;
            prefix = prefix.Trim();
            for (int i = 0; i < prefix.Length; i++)
            {
                if (children.TryGetValue(prefix[i], out var childNode))
                {
                    children = childNode.Children;
                    if (i == prefix.Length - 1 && childNode.IsKey)
                        yield return (prefix, childNode.Info);
                }

            }

            foreach (var node in children)
            {
                var stb = new StringBuilder(prefix);
                var stack = new Stack<Entry>();
                     stack.Push(new Entry(node, null));

                while (stack.Count != 0)
                {
                    var item = stack.Pop();
                    if (!string.IsNullOrEmpty(item.Prefix))
                        stb = stb.Clear().Append(item.Prefix);

                    stb.Append(item.TreeNode.Value);
                    if (item.TreeNode.IsKey)
                        yield return (stb.ToString(), item.TreeNode.Info);

                    foreach (var child in item.TreeNode.Children)
                    {
                        stack.Push(new Entry(child, item.TreeNode.Children.Count > 1 ? stb.ToString() : null));
                    }
                }
            }
        }

        [DebuggerDisplay("Value={TreeNode.Value}, Prefix={Prefix}")]
        private struct Entry
        {
            public TreeNode TreeNode { get; set; }
            public string? Prefix { get; set; }

            public Entry(TreeNode treeNode, string? prefix)
            {
                TreeNode = treeNode;
                Prefix = prefix;
            }
        }
    }
}
