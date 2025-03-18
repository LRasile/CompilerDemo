namespace POCOs.AbstractSyntaxTree
{
    public class PrintStatementNode : AbstractSyntaxTreeNode
    {
        public AbstractSyntaxTreeNode Expression { get; set; }

        public PrintStatementNode(AbstractSyntaxTreeNode expression)
        {
            Expression = expression;
        }
    }

}
