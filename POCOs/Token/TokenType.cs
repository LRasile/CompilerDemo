namespace POCOs.Token
{
    public enum TokenType
    {
        Identifier,  // e.g., "x", "myVariable", "userInput"
        Keyword,     // e.g., "let", "print", "if", "while", "return"
        Operator,    // e.g., "=", "+", "-", "*", "/", "==", "!="
        Number,      // e.g., "42", "3.14", "-100"
        Symbol,      // e.g., "{", "}", "(", ")"
        NewLine      // Represents a new line (not a visible token, but useful for tracking lines)

    }


}
