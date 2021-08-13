using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ProgrammingComp.Models
{
    public partial class TasksDBContext : DbContext
    {
        public TasksDBContext()
            : base("name=SubmitTaskSolution")
        {
        }

        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TasksResults> TasksResults { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
                .Property(e => e.taskName)
                .IsUnicode(false);

            modelBuilder.Entity<Task>()
                .Property(e => e.taskDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Task>()
                .Property(e => e.taskInputParams)
                .IsUnicode(false);

            modelBuilder.Entity<Task>()
                .Property(e => e.taskOutputParams)
                .IsUnicode(false);

            modelBuilder.Entity<TasksResults>()
               .Property(e => e.TaskID);

            modelBuilder.Entity<TasksResults>()
              .Property(e => e.Name)
              .IsUnicode(false);

            modelBuilder.Entity<TasksResults>()
             .Property(e => e.Result);


        }
    }
}
