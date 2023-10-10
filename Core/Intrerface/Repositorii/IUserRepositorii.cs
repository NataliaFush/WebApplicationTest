using Core.Intrerface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataBase
{
    public interface IUserRepositorii
    {
        public IUser GetUserById(Guid id);
        public bool CreateUser(IUser user);
    }
}
