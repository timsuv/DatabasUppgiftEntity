using DatabasUppgiftEntity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DatabasUppgiftEntity
{
    internal class MyDBCOntext : DbContext
    {
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Grade> Grade { get; set; }
        public DbSet<Course> Course { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=School;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
        }
    }
}
