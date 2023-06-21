using System.ComponentModel.DataAnnotations;

namespace FinSharp.model
{
    public class UserProfile
    {
        public UserProfile(Guid Id)
        {
            Id = Guid.NewGuid();
        }

        public UserProfile() { }

        public Guid Id { get; private set; }

        [Required]
        public string? Nickname { get; private set; }

        [Required]
        public int Age { get; private set; }

        [Required]
        public string? Photo { get; private set; }

        //region Methods
        public bool ValidateNickname(string nickname)
        {
            return true;
        }

        public bool ValidateAge(string age)
        {
            return true;
        }
    }
}
