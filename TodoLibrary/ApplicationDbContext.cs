using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TodoLibrary.Models;

namespace TodoLibrary
{
    /// <summary>
    /// Application DbContext to work with database.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<UserTasksModel> UserTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTasksModel>()
                .Property(t => t.IsFinished)
                .HasDefaultValue(0)
                .IsRequired();

            modelBuilder.Entity<UserTasksModel>()
                .Property(t => t.DateStarted)
                .HasDefaultValueSql("getdate()")
                .IsRequired();
        }
    }
}
