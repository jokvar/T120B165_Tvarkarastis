using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using T120B165.Models;


namespace T120B165.Data
{
    public class T120B165Context : DbContext
    {
        public T120B165Context(DbContextOptions<T120B165Context> options) : base(options)
        {
        }

        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Module> Modules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ModuleStudent>()
                .HasKey(t => new { t.ModuleID, t.StudentID });

            modelBuilder.Entity<ModuleStudent>()
                .HasOne(ms => ms.Module)
                .WithMany(m => m.ModuleStudents)
                .HasForeignKey(ms => ms.ModuleID);

            modelBuilder.Entity<ModuleStudent>()
                .HasOne(ms => ms.Student)
                .WithMany(s => s.ModuleStudents)
                .HasForeignKey(ms => ms.StudentID);
        }
    }
}
