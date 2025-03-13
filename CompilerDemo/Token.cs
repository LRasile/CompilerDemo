namespace CompilerDemo
{
    public class Token
    {
        public Token(string value, TokenType keyword)
        {
            Value = value;
            Type = keyword;
        }

        public string Value { get; set; }
        public TokenType Type { get; set; }
    }

    public enum TokenType
    {
        Identifier,  // e.g., "x", "myVariable", "userInput"
        Keyword,     // e.g., "let", "print", "if", "while", "return"
        Operator,    // e.g., "=", "+", "-", "*", "/", "==", "!="
        Number,      // e.g., "42", "3.14", "-100"
        Symbol,      // e.g., "{", "}", "(", ")"
        NewLine      // Represents a new line (not a visible token, but useful for tracking lines)

    }

    public class TokenPrinter
    {
        // Function to print the tokens in the desired format
        public static string PrintTokens(List<Token> tokens)
        {
            var output = "";

            foreach (var token in tokens)
            {
                output += $"[{token.Value}]";
                if(token.Type == TokenType.NewLine)
                {
                    output += "\n";                    
                }   

            }

            return output;
        }
    }


}
