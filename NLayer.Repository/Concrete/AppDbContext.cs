using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NLayer.Core.Concrate
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Output> Outputs { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<ClassBranch> ClassBranches { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<ExamAnswers> ExamAnswers { get; set; }
        public DbSet<ExamQuestions> ExamQuestions { get; set; }
        public DbSet<Guardian> Guardians { get; set; }
        public DbSet<Scors> Scors { get; set; }

    }



}

