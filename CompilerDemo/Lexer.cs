using System.Text.RegularExpressions;

namespace CompilerDemo
{
    public class Lexer
    {
        public List<Token> Tokenizer(string input)
        {
            var tokens = new List<Token>();

            // Split input into lines to capture line breaks
            var lines = input.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            foreach (var line in lines)
            {
                // Define a regular expression pattern for matching different tokens (ignoring newlines for now)
                string pattern = @"(\d+|\w+|[{}()\[\]=\+\-\*/%!<>,;])";  // Match numbers, words, and symbols.

                var matches = Regex.Matches(line, pattern);

                foreach (Match match in matches)
                {
                    var tokenString = match.Value;
                    var token = new Token(tokenString, AssignTokenType(tokenString));
                    tokens.Add(token);
                }

                // After processing a line, add a "newLine" token to signify the end of the line
                tokens.Add(new Token("newLine", TokenType.NewLine));
            }

            return tokens;
        }

        private TokenType AssignTokenType(string tokenString)
        {
            // Expanded keywords to include more reserved words
            var keyWords = new HashSet<string>
            {
                "var", "print", "function", "if", "else", "while", "return"
            };

            // Expanded operators to include comparison operators and logical operators
            var operators = new HashSet<string>
            {
                "=", "+", "-", "/", "*", "%", "!=","==", "<", ">", "<=", ">=", "!", "&&", "||"
            };

            // Expanded symbols to include additional punctuation or structural symbols
            var symbols = new HashSet<string>
            {
                "{", "}", "[", "]", "(", ")", ",", ";", ".", ":"
            };

            // Check for keywords
            if (keyWords.Contains(tokenString))
                return TokenType.Keyword;

            // Check for operators
            else if (operators.Contains(tokenString))
                return TokenType.Operator;

            // Check for symbols
            else if (symbols.Contains(tokenString))
                return TokenType.Symbol;

            // Check for numbers
            else if (int.TryParse(tokenString, out _))
                return TokenType.Number;

            // Default to Identifier (can be used for variable names, function names, etc.)
            return TokenType.Identifier;
        }

    }
}
