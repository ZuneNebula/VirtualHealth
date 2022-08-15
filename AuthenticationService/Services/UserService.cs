using AuthenticationService.Config;
using AuthenticationService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Services
{
    public class UserService
    {
        private readonly DataContext dataContext;
        public UserService(DataContext dataContext)
        {
            this.dataContext = dataContext; 
        }

        public async Task<User> GetAsync(string email)
        {
            var user = await dataContext.Users.FindAsync(email);

            return user;
        }

        public async Task<List<User>> GetAsync()
        {
            var users = await dataContext.Users.ToListAsync();

            return users;
        }

        public async Task<User> CreateAsync(User user)
        {
            dataContext.Users.Add(user);

            await dataContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateAsync(String email, User user)
        {
            var dbUser = await dataContext.Users.FindAsync(email);

            dbUser.Password = user.Password;

            await dataContext.SaveChangesAsync();

            return dbUser;
        }

        public async Task<bool> DeleteAsync(String email)
        {
            var user = await dataContext.Users.FindAsync(email);

            dataContext.Remove(user);

            await dataContext.SaveChangesAsync();

            return true;
        }
    }
}
