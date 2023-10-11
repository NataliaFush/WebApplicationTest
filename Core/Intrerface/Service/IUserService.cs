using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Intrerface.Service
{
    public interface IUserService
    {
        IUser GetUserById(Guid id);
        bool CreateUser(IUser user);
        bool IsUsedEmail(string? Email);
        public IEnumerable<IUser> GetAllUser();
    }
}
