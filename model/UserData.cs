using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FinSharp.model
{
    public class UserData
    {
        public UserData()
        {
        }

        public UserData(string? email, string? password, string? cpf, string? phone)
        {
            Email = email;
            Password = password;
            Cpf = cpf;
            Phone = phone;
        }

        // [Required]
        public string? Email { get; private set; }

        // [Required]
        public string? Password { get; private set; }

        // [Required]
        public string? Cpf { get; private set; }

        // [Required]
        public string? Phone { get; private set; }

        public bool ValidateCpf(string cpf)
        {
            Regex rgx = new Regex("^[0-9]{3}.?[0-9]{3}.?[0-9]{3}-?[0-9]{2}");
            if (string.IsNullOrEmpty(cpf))
            {
                return false;
            }
            else
            {
                var Test = rgx.Match(cpf);

                if (Test.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public string VaLidateEmail(string email)
        {
            return email;
        }

        public string ValidatePassword(string password)
        {
            return password;
        }

        public string ValidatePhone(string phone)
        {
            return phone;
        }
    }
}
