using AuthenticationService.Models;

namespace AuthenticationService.Services
{
    public interface IUserService
    {
        Task<User> GetAsync(string email);
        Task<List<User>> GetAsync();
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(String email, User user);
        Task<bool> DeleteAsync(String email);
    }
}
