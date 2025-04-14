using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Auth
{
    public interface IAuthRepository
    {
        User GetUser(int id);
        IEnumerable<User> GetAllUsers();
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}
