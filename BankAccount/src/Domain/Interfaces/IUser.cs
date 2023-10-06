using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    internal interface IUser
    {
        public Task<User> GetUserAsync(int id);
        public Task<User> GetUserByNameAsync(string name);
    }
}
