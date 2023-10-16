using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Repository
{
    public interface IDayRepository
    {
        IDictionary<DayType, string> GetDaysByLanguage(Language language);

    }
}