namespace FinSharp.schemas;

public class LoginWithEmailSchema
{
    public LoginWithEmailSchema(string? email, string? password)
    {
        Email = email;
        Password = password;
    }

    public string? Email { get; private set; }
    public string? Password { get; internal set; }
}