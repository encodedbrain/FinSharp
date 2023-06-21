namespace FinSharp.model
{
    abstract class ValidationsUser
    {
        public abstract bool ValidateCPf(string? email, string? password, string? cpf);
    }
}
