using Microsoft.EntityFrameworkCore;
using QuizzApplication.Classess;
using QuizzApplication.Entity.Classess;
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
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuestionBase> Questions { get; set; }
        public DbSet<PlayerResult> PlayerResults { get; set; }

        public QuizDBContext() => Database.EnsureCreated();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MultyQuestion>()
                .Property(q => q.Answers)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList()
            );

            modelBuilder.Entity<PlayerResult>()
                .HasKey(pr => new { pr.QuizID, pr.UserID });

            modelBuilder.Entity<PlayerResult>()
                .HasOne(pr => pr.Quiz)
                .WithMany(q => q.PlayerResults)
                .HasForeignKey(pr => pr.QuizID);

            modelBuilder.Entity<PlayerResult>()
                .HasOne(pr => pr.User)
                .WithMany()
                .HasForeignKey(pr => pr.UserID);

            modelBuilder.Entity<QuestionBase>().ToTable("Questions");
            modelBuilder.Entity<Question>().HasBaseType<QuestionBase>();
            modelBuilder.Entity<MultyQuestion>().HasBaseType<QuestionBase>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = question.db");
        }
    }
}
