using System;
using Core.Interface;
using Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MyDataBase.Models;

namespace MyDataBase
{

    public class UserRepository : IUserRepository
    {
        protected readonly MyDbContext _dbcontext;

        public UserRepository(MyDbContext myDb)
        {
            _dbcontext = myDb;
        }
        private IUser? CastToIUser(Models.User? user)
        {
            if (user == null) return null;

            return new Core.Entities.User()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Age = user.Age,
                CreateData = user.CreateData,
                Password = user.Password,
                Address = CastToIAddress(user.Address)
            };

        }
        private IAddress? CastToIAddress(Models.Address? address)
        {
            if (address == null) return null;

            return new Core.Entities.Address()
            {
                Id = address.Id,
                City = address.City,
                PostalCode = address.PostalCode,
                Street = address.Street
            };

        }

        public IEnumerable<IUser> GetAllUser()
        {
            return _dbcontext.Users.Include(x => x.Address).ToList().Select(CastToIUser);
            //var result = new List<IUser?>();
            //foreach (var user in users)
            //{
            //    result.Add(CastToIUser(user));
            //}
            //return result;
        }

        public IUser? GetUserById(Guid id)
        {
            var users = _dbcontext.Users;
            return CastToIUser(users.Include(x => x.Address).Where(x => x.Id == id).FirstOrDefault());
            //foreach (var user in users)
            //{
            //    if (user.Id == id)
            //    {
            //        return CastToIUser(user);
            //    }

            //}
            //return null;
        }

        public IUser? GetUserByEmail(string email)
        {
            var users = _dbcontext.Users;
            return CastToIUser(users.FirstOrDefault(x => x.Email != null && email != null && x.Email.ToLower() == email.ToLower()));

            //foreach (var user in users)
            //{
            //    if (!string.IsNullOrEmpty(user.Email) && (user.Email.ToLower().Equals(email.ToLower())))
            //    {
            //        return CastToIUser(user);
            //    }

            //}
            //return null;
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

        public bool UpdateUser(IUser user)
        {
            try
            {
                var dbUser = _dbcontext.Users.FirstOrDefault(x => x.Id == user.Id);
                if (dbUser == null)
                {
                    return false;
                }
                dbUser.Name = user.Name;
                dbUser.Age = user.Age;
                dbUser.Password = user.Password;
                if (user.Address != null)
                {
                    var dbAddress = _dbcontext.Address.FirstOrDefault(x => x.Id == user.Address.Id);
                    if (dbAddress != null && dbAddress.Id != 0)
                    {
                        dbAddress.Street = user.Address.Street;
                        dbAddress.City = user.Address.City;
                        dbAddress.PostalCode = user.Address.PostalCode;
                    }
                    else
                    {
                        dbUser.Address = new Models.Address
                        {
                            Street = user.Address.Street,
                            City = user.Address.City,
                            PostalCode = user.Address.PostalCode
                        };
                    }
                }

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
