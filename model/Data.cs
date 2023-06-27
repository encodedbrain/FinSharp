namespace FinSharp.model
{
    public class CPF
    {
        public CPF(string? email, string? password, string? cpf, string? phone)
        {
            Email = email;
            Password = password;
            Cpf = cpf;
            Phone = phone;
        }

        public string? Email { get; private set; }

        public string? Password { get; private set; }

        public string? Cpf { get; private set; }

        public string? Phone { get; private set; }
    }
}