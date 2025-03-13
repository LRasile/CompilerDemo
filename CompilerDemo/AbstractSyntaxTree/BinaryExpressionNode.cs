namespace CompilerDemo.AbstractSyntaxTree
{
    public class BinaryExpressionNode : AbstractSyntaxTreeNode
    {
        public AbstractSyntaxTreeNode Left { get; set; }
        public string Operator { get; set; } 
        public AbstractSyntaxTreeNode Right { get; set; }

        public BinaryExpressionNode(AbstractSyntaxTreeNode left, string operatorSymbol, AbstractSyntaxTreeNode right)
        {
            Left = left;
            Operator = operatorSymbol;
            Right = right;
        }
    }

}
