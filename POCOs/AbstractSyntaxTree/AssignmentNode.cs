namespace POCOs.AbstractSyntaxTree
{
    public class AssignmentNode : AbstractSyntaxTreeNode
    {
        public string VariableName { get; set; }
        public AbstractSyntaxTreeNode Expression { get; set; }

        public AssignmentNode(string variableName, AbstractSyntaxTreeNode expression)
        {
            VariableName = variableName;
            Expression = expression;
        }
    }

}
