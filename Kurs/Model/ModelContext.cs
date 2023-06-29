using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Kurs.Model
{
    public class ModelContext : DbContext
    {
        public DbSet<User> User { get; set; } = null;
        public DbSet<Worker> Worker { get; set; } = null;
        public DbSet<Commission> Commission { get; set; } = null;
        public DbSet<Meting> Meting { get; set; } = null;
        public DbSet<Visit> Visit { get; set; } = null;
        public DbSet<Members> Members { get; set; } = null;

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Duma.db");
        }
    }
}
