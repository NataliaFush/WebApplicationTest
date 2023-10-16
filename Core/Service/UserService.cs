using Core.Entities;
using Core.Interface;
using Core.Interface.Service;
using MyDataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public class UserService : IUserService
    {
        protected readonly IUserRepository _userRepositorii;
        public UserService(IUserRepository userRepositorii)
        {
            _userRepositorii = userRepositorii;
        }
        public IUser GetUserById(Guid id)
        {
            return _userRepositorii.GetUserById(id);
        }

        public bool IsUsedEmail(string? Email)
        {
            if (_userRepositorii.GetUserByEmail(Email) != null)
                return true;
            return false;
        }


        public bool CreateUser(IUser user)
        {
            user.Id = Guid.NewGuid();
            user.CreateData = DateTime.Now;
            return _userRepositorii.CreateUser(user);
        }

        public IEnumerable<IUser> GetAllUser()
        {
           return  _userRepositorii.GetAllUser();
        }
    }
}
