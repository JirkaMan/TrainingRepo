using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class PeopleContext:DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Adresses { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Company> Companies { get; set; }

        public override EntityEntry Add(object entity)
        {
            return base.Add(entity);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PeopleDb2;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

    }
}
