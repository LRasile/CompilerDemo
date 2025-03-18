namespace POCOs.AbstractSyntaxTree
{
    public class VariableDeclarationNode : AbstractSyntaxTreeNode
    {
        public string VariableName { get; set; }
        public AbstractSyntaxTreeNode Expression { get; }

        public VariableDeclarationNode(string variableName, AbstractSyntaxTreeNode expression)
        {
            VariableName = variableName;
            Expression = expression;
        }
    }

}
