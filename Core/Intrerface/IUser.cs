using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Intrerface
{
    public interface IUser
    {

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int? Age { get; set; }
        public DateTime? CreateData { get; set; }
        public string? Password { get; set; }
    }
}
