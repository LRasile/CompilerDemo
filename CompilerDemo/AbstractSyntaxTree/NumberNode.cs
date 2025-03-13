namespace CompilerDemo.AbstractSyntaxTree
{
    public class NumberNode : AbstractSyntaxTreeNode
    {
        public int Value { get; set; }

        public NumberNode(int value)
        {
            Value = value;
        }
    }

}
