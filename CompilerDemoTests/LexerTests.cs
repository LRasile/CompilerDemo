using CompilerDemo;
using POCOs.Token;

namespace CompilerDemoTests
{
    public class LexerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("", 1)]
        [TestCase("\r\n", 2)]
        [TestCase("var x = 5 + 3 * 2\r\nprint x", 12)]
        [TestCase("var x = (5 + 3) * 2\r\nprint x", 14)]
        public void ValidTests(string codeInput, int numberOfTokens)
        {
            // Arrange
            var lexer = new Lexer();

            // Act
            var tokens = lexer.Tokenizer(codeInput);
            var output = TokenPrinter.PrintTokens(tokens);

            // Assert
            Assert.That(tokens.Count, Is.EqualTo(numberOfTokens));
        }
    }
}