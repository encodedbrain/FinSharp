using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FinSharp.model
{
    public class UserData
    {
        public UserData() { }

        public UserData(string? email, string? password, string? cpf, string? phone)
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

        public bool ValidateCpf(string? cpf)
        {
            string RegularExpression = "^[0-9]{3}.?[0-9]{3}.?[0-9]{3}-?[0-9]{2}";

            Regex rgx = new Regex(RegularExpression);

            List<int> CpfDigits = new List<int>();
            List<int> NineDigitMultiplication = new List<int>();
            List<int> TenDigitMultiplication = new List<int>();

            if (string.IsNullOrEmpty(cpf))
            {
                return false;
            }
            else
            {
                Match CheckingFormat = rgx.Match(cpf);

                if (CheckingFormat.Success)
                {
                    string CpfFormated = Regex.Replace(cpf, "[^0-9a-zA-Z]+", "");

                    char[] ArrayChars = CpfFormated.ToCharArray();



                    for (int i = 0; i < 9; i++)
                    {
                        var characters = (int)Char.GetNumericValue(ArrayChars[i]);
                        CpfDigits.Add(characters);

                    }


                    for (int i = 0; i < CpfDigits.Count; i++)
                    {
                        var mult = CpfDigits[i] * (10 - i);

                        NineDigitMultiplication.Add(mult);
                    }


                    var CalculateSumOfNine = NineDigitMultiplication.Aggregate(SumOfNineDigits);



                    int SumOfNineDigits(int ac, int c)
                    {
                        return ac + c;
                    }
                    int NineDigit = (CalculateSumOfNine * 10) % 11;


                    if (NineDigit > 9)
                    {
                        NineDigit = 0;
                        if (Char.GetNumericValue(CpfFormated[9]) == NineDigit)
                        {


                            CpfDigits.Add(0);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (Char.GetNumericValue(CpfFormated[9]) == NineDigit)
                    {

                        CpfDigits.Add(NineDigit);

                    }
                    else
                    {
                        return false;
                    }


                    for (int i = 0; i < CpfDigits.Count; i++)
                    {
                        TenDigitMultiplication.Add(CpfDigits[i] * (11 - i));
                    }

                    var CalculateSumOfTen = TenDigitMultiplication.Aggregate(SumOfTenDigits);


                    int SumOfTenDigits(int ac, int c)
                    {
                        return ac + c;
                    }

                    var TenDigit = (CalculateSumOfTen * 10) % 11;


                    if (TenDigit > 9)
                    {
                        TenDigit = 0;
                        if (Char.GetNumericValue(CpfFormated[10]) == TenDigit)
                        {


                            CpfDigits.Add(0);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (Char.GetNumericValue(CpfFormated[10]) == TenDigit)
                    {

                        CpfDigits.Add(TenDigit);

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
