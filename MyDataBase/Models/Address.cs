using Core.Intrerface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataBase.Models
{
    internal class Address : IAddress
    {
        public int Id { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }

        [Required]
        public string? PostalCode { get; set;}

    }
}
