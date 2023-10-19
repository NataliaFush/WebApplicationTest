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
            var days = _myDbContext.Days;
            var dictionary = new Dictionary<DayType, string>();
            foreach (var day in days)
            {
                if (day.Language == language)
                {
                    dictionary.Add(day.DayOfWeek, day.Value);
                }
            }
            return dictionary;
        }
    }
}