using CompilerDemo.AbstractSyntaxTree;
namespace CompilerDemo
{
    public class Parser
    {
        private int _index = 0;
        private List<Token> _tokens;

        public Parser(List<Token> tokens)
        {
            _tokens = tokens;
        }

        public List<AbstractSyntaxTreeNode> Parse()
        {
            var nodes = new List<AbstractSyntaxTreeNode>();

            while (_index < _tokens.Count)
            {
                var token = _tokens[_index];

                if (token.Type == TokenType.Keyword)
                {
                    var node = ParseKeywordDeclaration();
                    if (node != null)
                    {
                        nodes.Add(node);
                    }
                }
                else
                {
                    _index++; // Skip unknown tokens
                }
            }

            return nodes;
        }

        private AbstractSyntaxTreeNode? ParseKeywordDeclaration()
        {
            string keyword = _tokens[_index].Value;
            _index++;

            if (keyword == "var")
            {
                return ParseVariableDeclaration();
            }
            else if (keyword == "print")
            {
                return ParsePrintStatement();
            }

            throw new Exception($"Unexpected keyword '{keyword}' at position {_index}.");
        }

        private VariableDeclarationNode ParseVariableDeclaration()
        {
            if (_tokens[_index].Type != TokenType.Identifier)
                throw new Exception($"Syntax error: Expected variable name at position {_index}.");

            string varName = _tokens[_index].Value;
            _index++;

            if (_tokens[_index].Type != TokenType.Operator || _tokens[_index].Value != "=")
                throw new Exception($"Syntax error: Expected '=' after variable name at position {_index}.");

            _index++;

            // Parse an expression (e.g., "5 + 3")
            var expression = ParseExpression();

            // Handle the end of the statement (new line indicates end)
            if (_tokens[_index].Type == TokenType.NewLine)
            {
                _index++; // Consume new line
            }
            else
            {
                throw new Exception($"Syntax error: Expected new line at position {_index}.");
            }

            return new VariableDeclarationNode(varName, expression);
        }

        private PrintStatementNode ParsePrintStatement()
        {
            // Parse the expression that follows the print statement
            var expression = ParsePrimaryExpression(); // This will now parse complex expressions like "5 + 3"

            // Handle the end of the statement (new line indicates end)
            if (_tokens[_index].Type == TokenType.NewLine)
            {
                _index++; // Consume the new line
            }
            else
            {
                throw new Exception($"Syntax error: Expected new line at position {_index}.");
            }

            return new PrintStatementNode(expression);
        }

        private AbstractSyntaxTreeNode ParseExpression()
        {
            return ParseAdditionSubtraction();
        }

        private AbstractSyntaxTreeNode ParseAdditionSubtraction()
        {
            var left = ParseMultiplicationDivision();

            while (_tokens[_index].Type == TokenType.Operator &&
                   (_tokens[_index].Value == "+" || _tokens[_index].Value == "-"))
            {
                var op = _tokens[_index].Value;
                _index++;
                var right = ParseMultiplicationDivision();
                left = new BinaryExpressionNode(left, op, right);
            }

            return left;
        }

        private AbstractSyntaxTreeNode ParseMultiplicationDivision()
        {
            var left = ParsePrimaryExpression();

            while (_tokens[_index].Type == TokenType.Operator &&
                   (_tokens[_index].Value == "*" || _tokens[_index].Value == "/"))
            {
                var op = _tokens[_index].Value;
                _index++;
                var right = ParsePrimaryExpression();
                left = new BinaryExpressionNode(left, op, right);
            }

            return left;
        }

        private AbstractSyntaxTreeNode ParsePrimaryExpression()
        {
            if (_tokens[_index].Type == TokenType.Number)
            {
                var number = _tokens[_index].Value;
                _index++; // Consume number
                return new NumberNode(int.Parse(number));
            }

            if (_tokens[_index].Type == TokenType.Identifier)
            {
                var identifier = _tokens[_index].Value;
                _index++; // Consume identifier
                return new VariableReferenceNode(identifier);
            }

            if (_tokens[_index].Type == TokenType.Symbol && _tokens[_index].Value == "(")
            {
                _index++; // Consume '('
                var innerExpr = ParseExpression();

                if (_tokens[_index].Type == TokenType.Symbol && _tokens[_index].Value == ")")
                {
                    _index++; // Consume ')'
                    return new ParenthesisExpressionNode(innerExpr);
                }
                else
                {
                    throw new Exception($"Syntax error: Expected ')' at position {_index}.");
                }
            }

            throw new Exception($"Syntax error: Unexpected token at position {_index}.");
        }
    }
}


