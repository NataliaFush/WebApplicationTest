using Core.Enums;
using Core.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Week : IWeek
    {
        private Dictionary<DayType, string> _week;
        public Week(IEnumerable<KeyValuePair<DayType, string>> week)
        {
            _week = new Dictionary<DayType, string>(week);
        }
        public string this[DayType dayOfWeek]
        {
            get
            {
                if (_week.ContainsKey(dayOfWeek))
                {
                    return _week[dayOfWeek];
                }
                else
                {
                    return "I dont know the day";
                }
            }
        }

        public string this[int dayOfWeek]
        {
            get
            {
                return this[(DayType)dayOfWeek];
            }
        }

        public string this[string dayOfWeek]
        {
            get
            {
                var day = Enum.Parse<DayType>(dayOfWeek);
                return this[day];
            }
        }
    }
}