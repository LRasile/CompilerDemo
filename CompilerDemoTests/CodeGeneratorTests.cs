using CompilerDemo;

namespace CompilerDemoTests
{
    public class CodeGeneratorTests
    {
        [Test]
        public void GenerateCodeTest()
        {
            // Arrange
            var inputString = "var x = 10\r\nvar y = x + 5\r\nprint y";
            var lexer = new Lexer();
            var tokens = lexer.Tokenizer(inputString);
            var parser = new Parser();
            var codeGenerator = new CodeGenerator();
            var nodes = parser.Parse(tokens);

            // Act
            var code = codeGenerator.GenerateCode(nodes);

            // Assert
            Assert.That(code, Is.EqualTo(@"using System;
class Program
{
    static void Main()
    {
        var x = 10;
        var y = x + 5;
        Console.WriteLine(y);
    }
}"));
        }
    }
}
