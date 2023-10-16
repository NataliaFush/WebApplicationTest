using Microsoft.EntityFrameworkCore;
using MyDataBase.Models;

namespace MyDataBase
{
    public class MyDbContext : DbContext
    {
        internal DbSet<User> Users { get; set; }
        internal DbSet<Address> Address { get; set; }
        internal DbSet<Core.Entities.Day> Days { get; set; }
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }
    }
}