using CompilerDemo;

var lexer = new Lexer();
var parser = new Parser();
var codeGenerator = new CodeGenerator();
var codeRunner = new CodeRunner();
Console.Write("Enter the file path: ");
var filePath = Console.ReadLine();

if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
{
    Console.WriteLine("Error: Invalid file path or file not found.");
    return;
}

using var reader = new StreamReader(filePath);
var inputString = await reader.ReadToEndAsync();
var tokens = lexer.Tokenizer(inputString);
var nodes = parser.Parse(tokens);
var code = codeGenerator.GenerateCode(nodes);

var fileName = Path.GetFileNameWithoutExtension(filePath);

await codeRunner.Run(code, fileName);
