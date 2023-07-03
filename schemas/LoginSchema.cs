namespace FinSharp.schemas
{
    public class LoginSchema
    {

        public LoginSchema(string? name, string? password)
        {
            Name = name;
            Password = password;
        }
        public string? Name { get; private set; }
        public string? Password { get; internal set; }
    }
}