namespace FinSharp.schemas;

public class TokenSchema
{
    public TokenSchema(string? token)
    {
        Token = token;
    }

    public string? Token { get; set; }
}