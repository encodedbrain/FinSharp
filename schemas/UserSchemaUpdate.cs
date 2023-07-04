namespace FinSharp.schemas
{
    public class UserUpdate
    {
        public UserUpdate(string? name, string? password, string newName, string newPassword, string newEmail)
        {
            Name = name;
            Password = password;
            NewName = newName;
            NewPassword = newPassword;
            NewEmail = newEmail;
        }

        public string? Name { get; set; }
        public string? Password { get; set; }


        public string? NewName { get; set; }
        public string? NewPassword { get; set; }
        public string? NewEmail { get; set; }
    }
}