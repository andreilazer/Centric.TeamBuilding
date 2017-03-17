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
            if (!Regex.IsMatch(Email, "^[A-Za-z0-9._%+-]+@centric.eu$"))
            {
                result.Message = "Invalid Email Address";
                return result;
            }

            result.IsValid = true;
            return result;
        }
    }
}