using Core.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDataBase.Models
{
    [Index(nameof(Language), nameof(DayOfWeek), IsUnique = true)]
    internal class Day
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public Language Language { get; set; }
        
        [Column(TypeName = "nvarchar(20)")]
        public DayType DayOfWeek { get; set; }
        [StringLength(20)]
        public string? Value { get; set; }
    }
}