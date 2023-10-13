using Core.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DayOfWeek = Core.Enum.DayOfWeek;

namespace MyDataBase.Models
{
    [Index(nameof(Language), nameof(DayOfWeek), IsUnique = true)]
    internal class Day
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName ="nvarchar(20)")]
        public Language Language { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public DayOfWeek DayOfWeek { get; set; }
        [StringLength(20)]
        public string? Value { get; set; }
    }
}
