using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCOs.AbstractSyntaxTree
{
    public class AbstractTreePrinter
    {
        // Main method to print the entire AST and return the formatted string
        public static string Print(List<AbstractSyntaxTreeNode> nodes)
        {
            if (nodes == null)
                return string.Empty;
         
            var result = new StringBuilder();
         
            foreach (var node in nodes)
            {
                result.AppendLine(Print(node));
            }
            return result.ToString();
        }


        public static string Print(AbstractSyntaxTreeNode node, int indentLevel = 0)
        {
            if (node == null)
                return string.Empty;
                        
            var result = new StringBuilder();
                        
            string indent = new string(' ', indentLevel * 2);
                        
            switch (node)
            {
                case VariableDeclarationNode varDeclNode:
                    result.AppendLine($"{indent}Variable Declaration: {varDeclNode.VariableName}");
                    result.Append(Print(varDeclNode.Expression, indentLevel + 1));
                    break;

                case PrintStatementNode printNode:
                    result.AppendLine($"{indent}Print Statement:");
                    result.Append(Print(printNode.Expression, indentLevel + 1));
                    break;

                case BinaryExpressionNode binOpNode:
                    result.AppendLine($"{indent}Binary Operation: {binOpNode.Operator}");
                    result.AppendLine($"{indent} Left:");
                    result.Append(Print(binOpNode.Left, indentLevel + 1));
                    result.AppendLine($"{indent} Right:");
                    result.Append(Print(binOpNode.Right, indentLevel + 1));
                    break;

                case ParenthesisExpressionNode parenNode:
                    result.AppendLine($"{indent}Parenthesis Expression:");
                    result.Append(Print(parenNode.Expression, indentLevel + 1));
                    break;

                case NumberNode numberNode:
                    result.AppendLine($"{indent}Number: {numberNode.Value}");
                    break;

                case VariableReferenceNode varRefNode:
                    result.AppendLine($"{indent}Variable Reference: {varRefNode.VariableName}");
                    break;

                default:
                    result.AppendLine($"{indent}Unknown Node: {node.GetType().Name}");
                    break;
            }

            return result.ToString();
        }
    }
}
