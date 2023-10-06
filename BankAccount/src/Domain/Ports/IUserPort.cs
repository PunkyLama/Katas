using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface IUserPort
    {
        public Task<User> GetUserAsync(int id);
        public Task<User> GetUserByNameAsync(string name);
    }
}
