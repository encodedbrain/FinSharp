namespace FinSharp.schemas
{

    public class UserSchema
    {
        public UserSchema(string? name, int age, string? email, string? password, string? cpf)
        {
            Name = name;
            Age = age;
            Email = email;
            Password = password;
            Cpf = cpf;
        }

        public string? Name { get; private set; }

        public int Age { get; private set; }

        public string? Email { get; private set; }

        public string? Password { get; internal set; }

        public string? Cpf { get; internal set; }
    }
}