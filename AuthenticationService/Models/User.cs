using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.Models
{
    public class User
    {
        [Key]
        public String Email { get; set; }

        public String Password { get; set; }

        public UserRole Role { get; set; }
    }
}
