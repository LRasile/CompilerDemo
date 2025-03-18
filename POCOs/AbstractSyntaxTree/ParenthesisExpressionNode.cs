namespace POCOs.AbstractSyntaxTree
{
    public class ParenthesisExpressionNode : AbstractSyntaxTreeNode
    {
        public AbstractSyntaxTreeNode Expression { get; }

        public ParenthesisExpressionNode(AbstractSyntaxTreeNode expression)
        {
            Expression = expression;
        }
    }

}
