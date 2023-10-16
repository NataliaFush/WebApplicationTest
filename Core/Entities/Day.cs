using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Core.Interface;
using Core.Enums;


namespace Core.Entities
{
    public class Day : IDay
    {
        public int Id { get; set; }
        public Language? Language { get; set; }
        public DayType? DayType { get; set; }
        public string? Value { get; set; }
        
    }
}