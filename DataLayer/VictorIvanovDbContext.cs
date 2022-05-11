using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class VictorIvanovDbContext : DbContext
    {
        public VictorIvanovDbContext() { }
        public VictorIvanovDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-54NFRM2\SQLEXPRESS;Database=VictorIvanov_11e_2;Trusted_Connection=True;");
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
