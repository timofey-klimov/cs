using System.Diagnostics;

namespace DataStructure.BorTree
{
    [DebuggerDisplay("Value = {Value}")]
    public class TreeNode
    {
        public char Value { get; private set; }
        public object? Info { get; private set; }
        public bool IsKey { get; private set; }

        public HashSet<TreeNode> Children { get; init; } = new HashSet<TreeNode>();

        public TreeNode(char value, object? info, bool isKey) => (Value, Info, IsKey) = (value, info, isKey);

        public static implicit operator TreeNode(char value) => new TreeNode(value, null, false);

        public void SetKey(object? info)
        {
            IsKey = true;
            Info = info;
        }

        public override int GetHashCode() => Value.GetHashCode();
        public override bool Equals(object? obj) => obj is TreeNode node && node.Value.Equals(Value);
    }
}
