namespace POCOs.Token
{
    public class Token
    {
        public Token(string value, TokenType keyword)
        {
            Value = value;
            Type = keyword;
        }

        public Token()
        {
                
        }

        public string Value { get; set; }
        public TokenType Type { get; set; }
    }

 
}
