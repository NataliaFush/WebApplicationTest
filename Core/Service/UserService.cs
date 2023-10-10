using Core.Entities;
using Core.Intrerface;
using Core.Intrerface.Service;
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
        protected readonly IUserRepositorii _userRepositorii;
        public UserService(IUserRepositorii userRepositorii)
        {
            _userRepositorii = userRepositorii;
        }
        public IUser GetUserById(Guid id)
        {
            return _userRepositorii.GetUserById(id);
            //var user = _userRepositorii.GetUserById(id);
            //if (user is User)
            //{
            //    return user as User;
            //}
            //else
            //{
            //    return new User()
            //    {
            //        Id = user.Id,
            //        Name = user.Name,
            //        Age = user.Age,
            //        CreateData = user.CreateData,
            //        Email = user.Email,
            //        Password = user.Password,
            //    };
            //}
            //return null;
        }

        public bool CreateUser(IUser user)
        {
            user.Id = Guid.NewGuid();
            user.CreateData = DateTime.Now;
            return _userRepositorii.CreateUser(user);
        }
    }
}
