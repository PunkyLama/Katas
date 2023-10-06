using Domain.Models;
using Domain.Ports;

namespace Infrastructure
{
    public class UserRepository : IUserPort
    {
        public UserRepository() { }

        public Task<User> GetUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}