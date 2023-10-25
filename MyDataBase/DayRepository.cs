using Core.Enums;
using Core.Interface.Repository;
using MyDataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class DayRepository : IDayRepository
    {
        protected readonly MyDbContext _myDbContext;
        public DayRepository(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }
        public IDictionary<DayType, string> GetDaysByLanguage(Language language)
        {
            return _myDbContext.Days
                .Where(x => x.Language == language)
                .ToDictionary(x => x.DayOfWeek, x => x.Value);
        }
    }
}