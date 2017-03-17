using System;
using System.Text.RegularExpressions;

namespace Centric.TeamBuilding.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public UserRoles Role { get; set; }

        public Gender Gender { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public ValidationResult Validate()
        {
            var result = new ValidationResult() {IsValid = false};
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(Email) ||
                string.IsNullOrEmpty(Password))
            {
                result.Message = "Fill all required fields";
                return result;
            }
            if (Password.Length < 6)
            {
                result.Message = "Password too short";
                return result;
            }
            if (!Regex.IsMatch(Email,
                "^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&\'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$"))
            {
                result.Message = "Invalid Email Address";
                return result;
            }

            result.IsValid = true;
            return result;
        }
    }
}