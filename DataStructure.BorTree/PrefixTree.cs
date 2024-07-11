using System.Diagnostics;
using System.Text;

namespace DataStructure.BorTree
{
    public class PrefixTree
    {

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
        [DebuggerDisplay("Value={StackKey.Value}, Prefix={Prefix}")]
        private record StackItem(TreeNode StackKey, string? Prefix);

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
                var stack = new Stack<StackItem>();
                     stack.Push(new StackItem(node, null));

                while (stack.Count != 0)
                {
                    var item = stack.Pop();
                    if (!string.IsNullOrEmpty(item.Prefix))
                        stb = new StringBuilder(item.Prefix);

                    stb.Append(item.StackKey.Value);
                    if (item.StackKey.IsKey)
                        yield return (stb.ToString(), item.StackKey.Info);

                    foreach (var child in item.StackKey.Children)
                    {
                        stack.Push(new StackItem(child, item.StackKey.Children.Count > 1 ? stb.ToString() : null));
                    }
                }
            }
        }
    }
}
