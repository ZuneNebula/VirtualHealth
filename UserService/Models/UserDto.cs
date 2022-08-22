namespace UserService.Models
{
    public class UserDto
    {
        public String Email { get; set; }

        public String Password { get; set; }

        public UserRoleDto Role { get; set; }
    }
}
