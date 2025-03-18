using POCOs.Token;
using System.Text.RegularExpressions;

namespace CompilerDemo
{
    public class Lexer
    {
        public List<Token> Tokenizer(string input)
        {
            var tokens = new List<Token>();
                        
            var lines = input.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            foreach (var line in lines)
            {                
                string pattern = @"(\d+|\w+|[{}()\[\]=\+\-\*/%!<>,;])";  

                var matches = Regex.Matches(line, pattern);

                foreach (Match match in matches)
                {
                    var tokenString = match.Value;
                    var token = new Token(tokenString, AssignTokenType(tokenString));
                    tokens.Add(token);
                }

                tokens.Add(new Token("newLine", TokenType.NewLine));
            }

            return tokens;
        }

        private TokenType AssignTokenType(string tokenString)
        {
            var keyWords = new HashSet<string>
            {
                "var", "print", "function", "if", "else", "while", "return"
            };

            var operators = new HashSet<string>
            {
                "=", "+", "-", "/", "*", "%", "!=","==", "<", ">", "<=", ">=", "!", "&&", "||"
            };

            var symbols = new HashSet<string>
            {
                "{", "}", "[", "]", "(", ")", ",", ";", ".", ":"
            };

            if (keyWords.Contains(tokenString))
                return TokenType.Keyword;

            else if (operators.Contains(tokenString))
                return TokenType.Operator;

            else if (symbols.Contains(tokenString))
                return TokenType.Symbol;

            else if (int.TryParse(tokenString, out _))
                return TokenType.Number;

            return TokenType.Identifier;
        }

    }
}
