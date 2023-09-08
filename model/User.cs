using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace LinkShortener.model
{
    [Table("Users")]
    public class User
    {
        private enum Opt
        {
            Admin = 0,
            User = 1
        }


        public User(string? name, int age, string? email, string? password, string? cpf)
        {
            Name = name;
            Age = age;
            Email = email;
            Password = password;
            Role = (int)Opt.User;
            Cpf = cpf;
        }

        public User()
        {
            Id = new Guid();

            DateTime date = DateTime.Now;
            CreateAt = date.ToString("dd/MM/yyyy");
        }

        [Display(Name = "Id")][Column("Id")] public Guid Id { get; private set; }

        [Display(Name = "Name")]
        [Column("Name")]
        public string? Name { get; internal set; }

        [Display(Name = "Age")]
        [Column("Age")]
        public int Age { get; private set; }

        [Display(Name = "Email")]
        [Column("Email")]
        public string? Email { get; internal set; }

        [Display(Name = "Password")]
        [Column("Password")]
        public string? Password { get; internal set; }

        [Display(Name = "Role")]
        [Column("Role")]
        public int? Role { get; private set; }

        [Display(Name = "Cpf")]
        [Column("Cpf")]
        public string? Cpf { get; internal set; }

        [Display(Name = "CreateAt")]
        [Column("CreateAt")]
        public string? CreateAt { get; private set; } = DateTime.Now.ToString("dd/MM/yyyy");


        public bool ValidateCpf(string? cpf)
        {

            string pattern = "^[0-9]{3}.?[0-9]{3}.?[0-9]{3}-?[0-9]{2}";

            Regex rgx = new Regex(pattern);

            List<int> cpfDigits = new List<int>();
            List<int> nineDigitMultiplication = new List<int>();
            List<int> tenDigitMultiplication = new List<int>();

            if (string.IsNullOrEmpty(cpf))
            {
                return false;
            }
            else
            {
                Match checkingFormat = rgx.Match(cpf);

                if (checkingFormat.Success)
                {
                    string cpfFormated = Regex.Replace(cpf, "[^0-9a-zA-Z]+", "");

                    char[] arrayChars = cpfFormated.ToCharArray();


                    for (int i = 0; i < 9; i++)
                    {
                        var characters = (int)Char.GetNumericValue(arrayChars[i]);
                        cpfDigits.Add(characters);
                    }


                    for (int i = 0; i < cpfDigits.Count; i++)
                    {
                        var mult = cpfDigits[i] * (10 - i);

                        nineDigitMultiplication.Add(mult);
                    }


                    var calculateSumOfNine = nineDigitMultiplication.Aggregate(SumOfNineDigits);


                    int SumOfNineDigits(int ac, int c)
                    {
                        return ac + c;
                    }

                    var nineDigit = (calculateSumOfNine * 10) % 11;


                    if (nineDigit > 9)
                    {
                        nineDigit = 0;
                        if (Char.GetNumericValue(cpfFormated[9]) == nineDigit)
                        {
                            cpfDigits.Add(0);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (Char.GetNumericValue(cpfFormated[9]) == nineDigit)
                    {
                        cpfDigits.Add(nineDigit);
                    }
                    else
                    {
                        return false;
                    }


                    for (var i = 0; i < cpfDigits.Count; i++)
                    {
                        tenDigitMultiplication.Add(cpfDigits[i] * (11 - i));
                    }

                    var calculateSumOfTen = tenDigitMultiplication.Aggregate(SumOfTenDigits);


                    int SumOfTenDigits(int ac, int c)
                    {
                        return ac + c;
                    }

                    var tenDigit = (calculateSumOfTen * 10) % 11;


                    if (tenDigit > 9)
                    {
                        tenDigit = 0;
                        if (Char.GetNumericValue(cpfFormated[10]) == tenDigit)
                        {
                            cpfDigits.Add(0);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (Char.GetNumericValue(cpfFormated[10]) == tenDigit)
                    {
                        cpfDigits.Add(tenDigit);
                    }
                    else
                    {
                        return false;
                    }


                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public string ReturnCpfFormated(string? cpf)
        {

            if (!ValidateCpf(cpf))
            {
                return "";
            }

            string cpfFormated = Regex.Replace(cpf, "[^0-9a-zA-Z]+", "");

            return cpfFormated;
        }

        public bool VaLidateEmail(string? email)
        {


            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            string pattern = "^\\S+@\\S+\\.\\S+$";
            Regex rgx = new Regex(pattern);


            return rgx.IsMatch(email);
        }

        public string EncryptingPassword(string? password)

        {

            var hash = SHA1.Create();
            var encoding = new ASCIIEncoding();
            var arrayBytes = encoding.GetBytes(password);

            arrayBytes = hash.ComputeHash(arrayBytes);

            var strHex = new StringBuilder();

            foreach (var value in arrayBytes)
            {
                strHex.Append(value.ToString("x2"));
            }

            return strHex.ToString();
        }

        public bool ValidateName(string? name)
        {

            if (name.Length > 10) return false;
            return true;
        }

        public bool ValidateAge(int age)
        {
            if (age < 18) return false;
            return true;
        }
    }
}