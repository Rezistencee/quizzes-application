using Microsoft.EntityFrameworkCore;
using QuizzApplication.Classess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzApplication.Entity.Context
{
    public class QuizDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public QuizDBContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = question.db");
        }
    }
}
