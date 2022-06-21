using System.ComponentModel.DataAnnotations;

namespace Todos.Models
{
    public class RegisterNLoginModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$_%*?!&])[A-Za-z\d@$_!%*?&]{8,}$",
            ErrorMessage = "Minimum eight characters, at least one uppercase letter,"
             + "one lowercase letter, one number and one special character")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
