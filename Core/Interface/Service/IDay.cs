using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
    public interface IDay
    {
        int Id { get; set; }
        Language? Language { get; set; }
        DayType? DayType { get; set; }
        string? Value { get; set; }
    }
}