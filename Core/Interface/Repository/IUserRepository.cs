using Core.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataBase
{
    public interface IUserRepository
    {
        public IUser GetUserById(Guid id);
        public bool CreateUser(IUser user);
        public IUser GetUserByEmail(string email);
        public IEnumerable<IUser> GetAllUser();
        bool UpdateUser(IUser user);
    }
}
