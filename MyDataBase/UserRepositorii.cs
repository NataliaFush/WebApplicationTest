using System;
using Core.Intrerface;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataBase
{
    
    public class UserRepositorii : IUserRepositorii
    {
        private IUser? CastToIUser(Models.User user)
        {
            if (user == null) return null;

            return new Core.Entities.User()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Age = user.Age,
                CreateData = user.CreateData,
                Password = user.Password
            };

        }
        protected readonly MyDbContext _dbcontext;

        public UserRepositorii(MyDbContext myDb)
        {
            _dbcontext = myDb;
        }

        public IEnumerable<IUser> GetAllUser()
        {
            var users = _dbcontext.Users;
            var result = new List<IUser>();
            foreach (var user in users)
            {

                result.Add(CastToIUser(user));
            }
            return result;
        }

        public IUser GetUserById(Guid id)
        {
            var users = _dbcontext.Users;
            foreach (var user in users)
            {
                if (user.Id == id)
                {
                    return CastToIUser(user);
                }

            }
            return null;
        }

        public IUser GetUserByEmail(string email)
        {
            var users = _dbcontext.Users;
            foreach (var user in users)
            {
                if (!string.IsNullOrEmpty(user.Email) && (user.Email.ToLower().Equals(email.ToLower())))
                {
                    return CastToIUser(user);

                }

            }
            return null;
        }

        public bool CreateUser(IUser user)
        {
            try
            {
                _dbcontext.Users.Add(new Models.User
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Age = user.Age,
                    CreateData = user.CreateData,
                    Password = user.Password
                });
                _dbcontext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
