using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class AppConf
    {
        public TimeSpan ElapsedDbTime { get; set; }
        public TimeSpan ElapsedCacheTime { get; set; }
    }
}
