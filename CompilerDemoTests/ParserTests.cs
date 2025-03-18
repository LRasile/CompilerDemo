using CompilerDemo;
using POCOs.AbstractSyntaxTree;
using POCOs.Token;

namespace CompilerDemoTests
{
    public class ParserTests
    {

        [Test]
        public void VariableAndPrintTest()
        {
            // Arrange
            var inputString = "var x = 5\r\nprint x";
            var lexer = new Lexer();
            var tokens = lexer.Tokenizer(inputString);
            var output = TokenPrinter.PrintTokens(tokens);
            var parser = new Parser();

            // Act
            var ast = parser.Parse(tokens);

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public void ExpressionWithParenthesesTest()
        {
            // Arrange
            var inputString = "var x = (5 + 3) * 2\r\nprint x";
            var lexer = new Lexer();
            var tokens = lexer.Tokenizer(inputString);
            var parser = new Parser();

            // Act
            var ast = parser.Parse(tokens);

            // Assert
            Assert.That(ast.Count, Is.EqualTo(2)); // Should have two nodes
            Assert.IsInstanceOf<VariableDeclarationNode>(ast[0]);
            Assert.IsInstanceOf<PrintStatementNode>(ast[1]);

            var varNode = (VariableDeclarationNode)ast[0];
            Assert.That(varNode.VariableName, Is.EqualTo("x"));

            // Check that the Value is now a BinaryOperationNode
            Assert.IsInstanceOf<BinaryExpressionNode>(varNode.Expression);
            var binaryNode = (BinaryExpressionNode)varNode.Expression;

            // Ensure the left side of the BinaryOperationNode is a ParenthesisNode
            Assert.IsInstanceOf<ParenthesisExpressionNode>(binaryNode.Left);
            var parenthesisNode = (ParenthesisExpressionNode)binaryNode.Left;

            // Inside the ParenthesisNode, there should be another BinaryOperationNode (5 + 3)
            Assert.IsInstanceOf<BinaryExpressionNode>(parenthesisNode.Expression);
            var innerBinaryNode = (BinaryExpressionNode)parenthesisNode.Expression;

            // Ensure that the left and right of the inner binary node are numbers
            Assert.IsInstanceOf<NumberNode>(innerBinaryNode.Left);
            Assert.IsInstanceOf<NumberNode>(innerBinaryNode.Right);

            // Check the print node
            var printNode = (PrintStatementNode)ast[1];
            Assert.IsInstanceOf<VariableReferenceNode>(printNode.Expression);
            var variableNode = (VariableReferenceNode)printNode.Expression;
            Assert.That(variableNode.VariableName, Is.EqualTo("x"));
        }

        [Test]
        public void ExpressionWithMultipleOperationsTest()
        {
            // Arrange
            var inputString = "var x = (5 + 3) * 2 - 10\r\nprint x";
            var lexer = new Lexer();
            var tokens = lexer.Tokenizer(inputString);
            var parser = new Parser();

            // Act
            var ast = parser.Parse(tokens); 

            var output = TokenPrinter.PrintTokens(tokens);
            output += AbstractTreePrinter.Print(ast);

            // Assert
            Assert.That(ast.Count, Is.EqualTo(2)); // Should have two nodes
            Assert.IsInstanceOf<VariableDeclarationNode>(ast[0]);
            Assert.IsInstanceOf<PrintStatementNode>(ast[1]);

            var varNode = (VariableDeclarationNode)ast[0];
            Assert.That(varNode.VariableName, Is.EqualTo("x"));

            Assert.IsInstanceOf<BinaryExpressionNode>(varNode.Expression);
            var binaryNode = (BinaryExpressionNode)varNode.Expression;
            Assert.That(binaryNode.Operator, Is.EqualTo("-"));
            Assert.That(((NumberNode)binaryNode.Right).Value, Is.EqualTo(10));

            Assert.IsInstanceOf<BinaryExpressionNode>(binaryNode.Left);
            var binaryNode1 = (BinaryExpressionNode)binaryNode.Left;
            Assert.That(binaryNode1.Operator, Is.EqualTo("*"));
            Assert.That(((NumberNode)binaryNode1.Right).Value, Is.EqualTo(2));

            Assert.IsInstanceOf<ParenthesisExpressionNode>(binaryNode1.Left);
            var parenthesisNode = (ParenthesisExpressionNode)binaryNode1.Left;

            
            Assert.IsInstanceOf<BinaryExpressionNode>(parenthesisNode.Expression);
            var innerBinaryNode = (BinaryExpressionNode)parenthesisNode.Expression;
            Assert.That(innerBinaryNode.Operator, Is.EqualTo("+"));

            Assert.IsInstanceOf<NumberNode>(innerBinaryNode.Left);
            Assert.That(((NumberNode)innerBinaryNode.Left).Value, Is.EqualTo(5));
            Assert.IsInstanceOf<NumberNode>(innerBinaryNode.Right);
            Assert.That(((NumberNode)innerBinaryNode.Right).Value, Is.EqualTo(3));
                        

            // Check the print node
            var printNode = (PrintStatementNode)ast[1];
            Assert.IsInstanceOf<VariableReferenceNode>(printNode.Expression);
            var variableNode = (VariableReferenceNode)printNode.Expression;
            Assert.That(variableNode.VariableName, Is.EqualTo("x"));
        }


        [Test]
        public void ExpressionWithMultipleParenthesesTest()
        {
            // Arrange
            var inputString = "var x = (5 + 3) * (2 - 10)\r\nprint x";
            var lexer = new Lexer();
            var tokens = lexer.Tokenizer(inputString);
            var parser = new Parser();

            // Act
            var ast = parser.Parse(tokens);

            var output = TokenPrinter.PrintTokens(tokens);
            output += AbstractTreePrinter.Print(ast);

            // Assert
            Assert.That(ast.Count, Is.EqualTo(2)); // Should have two nodes
            Assert.IsInstanceOf<VariableDeclarationNode>(ast[0]);
            Assert.IsInstanceOf<PrintStatementNode>(ast[1]);

            var varNode = (VariableDeclarationNode)ast[0];
            Assert.That(varNode.VariableName, Is.EqualTo("x"));

            Assert.IsInstanceOf<BinaryExpressionNode>(varNode.Expression);
            var binaryNode = (BinaryExpressionNode)varNode.Expression;
            Assert.That(binaryNode.Operator, Is.EqualTo("*"));


            Assert.IsInstanceOf<ParenthesisExpressionNode>(binaryNode.Left);
            var parenthesisNodeLeft = (ParenthesisExpressionNode)binaryNode.Left;
            Assert.IsInstanceOf<ParenthesisExpressionNode>(binaryNode.Right);
            var parenthesisNodeRight = (ParenthesisExpressionNode)binaryNode.Right;

            Assert.IsInstanceOf<BinaryExpressionNode>(parenthesisNodeLeft.Expression);
            var innerBinaryNode = (BinaryExpressionNode)parenthesisNodeLeft.Expression;
            Assert.That(innerBinaryNode.Operator, Is.EqualTo("+"));

            Assert.IsInstanceOf<NumberNode>(innerBinaryNode.Left);
            Assert.That(((NumberNode)innerBinaryNode.Left).Value, Is.EqualTo(5));
            Assert.IsInstanceOf<NumberNode>(innerBinaryNode.Right);
            Assert.That(((NumberNode)innerBinaryNode.Right).Value, Is.EqualTo(3));



            Assert.IsInstanceOf<BinaryExpressionNode>(parenthesisNodeRight.Expression);
            var innerBinaryNodeRight = (BinaryExpressionNode)parenthesisNodeRight.Expression;
            Assert.That(innerBinaryNodeRight.Operator, Is.EqualTo("-"));

            Assert.IsInstanceOf<NumberNode>(innerBinaryNodeRight.Left);
            Assert.That(((NumberNode)innerBinaryNodeRight.Left).Value, Is.EqualTo(2));
            Assert.IsInstanceOf<NumberNode>(innerBinaryNodeRight.Right);
            Assert.That(((NumberNode)innerBinaryNodeRight.Right).Value, Is.EqualTo(10));


            // Check the print node
            var printNode = (PrintStatementNode)ast[1];
            Assert.IsInstanceOf<VariableReferenceNode>(printNode.Expression);
            var variableNode = (VariableReferenceNode)printNode.Expression;
            Assert.That(variableNode.VariableName, Is.EqualTo("x"));
        }


        [Test]
        public void SyntaxErrorInParenthesesTest()
        {
            // Arrange
            var inputString = "var x = (5 + 3 * 2\r\nprint x"; // Missing closing parenthesis
            var lexer = new Lexer();
            var parser = new Parser();
            var tokens = lexer.Tokenizer(inputString);

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => parser.Parse(tokens));
            Assert.That(ex.Message, Is.EqualTo("Syntax error: Expected ')' at position 9."));
        }

    }
}
