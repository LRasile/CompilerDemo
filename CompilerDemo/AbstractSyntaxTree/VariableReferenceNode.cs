namespace CompilerDemo.AbstractSyntaxTree
{
    public class VariableReferenceNode : AbstractSyntaxTreeNode
    {
        public string VariableName { get; }

        public VariableReferenceNode(string variableName)
        {
            VariableName = variableName;
        }
    }

}
