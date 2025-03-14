using CompilerDemo.AbstractSyntaxTree;
using System.Text;

namespace CompilerDemo
{
    public class CodeGenerator 
    {
        public string GenerateCode(List<AbstractSyntaxTreeNode> nodes)
        {
            var sb= new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("class Program");
            sb.AppendLine("{");
            sb.AppendLine("    static void Main()");
            sb.AppendLine("    {");

            foreach (var node in nodes)
            {
                sb.AppendLine($"        {GenerateNode(node)};");
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");
            return sb.ToString().TrimEnd();
        }

        private string GenerateNode(AbstractSyntaxTreeNode node)
        {
            switch (node)
            {
                case VariableDeclarationNode varDecl:
                    return $"var {varDecl.VariableName} = {GenerateNode(varDecl.Expression)}";

                case PrintStatementNode printNode:
                    return $"Console.WriteLine({GenerateNode(printNode.Expression)})";

                case BinaryExpressionNode binaryExpr:
                    return $"{GenerateNode(binaryExpr.Left)} {binaryExpr.Operator} {GenerateNode(binaryExpr.Right)}";

                case NumberNode numberNode:
                    return numberNode.Value.ToString();

                case VariableReferenceNode varRef:
                    return varRef.VariableName;

                case ParenthesisExpressionNode parenExpr:
                    return $"({GenerateNode(parenExpr.Expression)})";

                default:
                    throw new Exception($"Unknown node type: {node.GetType().Name}");
            }
        }
    }
}
