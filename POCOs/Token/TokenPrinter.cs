namespace POCOs.Token
{
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
