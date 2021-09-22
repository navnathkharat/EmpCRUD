using EmpAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpAPI.Context
{
    public class EmpDbContext : DbContext
    {
        public EmpDbContext()
        {

        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=EMPWEBAPI;Trusted_Connection=True;");
        }

 
    }
}
