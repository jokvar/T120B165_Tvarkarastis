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
        public DbSet<TimeTable> TimeTables { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<InformalGathering> InformalGatherings { get; set; }

        //many 2 many tables
        public DbSet<ModuleStudent> ModuleStudents { get; set; }
        public DbSet<LectureStudent> LectureStudents { get; set; }
        public DbSet<InformalGatheringTimeTable> StudentTimeTables { get; set; }
        public DbSet<LectureTimeTable> LectureTimeTables { get; set; }
        public DbSet<InformalGatheringStudent> InformalGatheringStudents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //no delete cascade on many2many
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().Where(e => !e.IsOwned()).SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            //correct many 2 many associations
            //student module selections
            modelBuilder.Entity<ModuleStudent>()
                .HasKey(x => new { x.ModuleID, x.StudentID });
            modelBuilder.Entity<ModuleStudent>()
                .HasOne(ms => ms.Module)
                .WithMany(m => m.Students)
                .HasForeignKey(ms => ms.StudentID);
            modelBuilder.Entity<ModuleStudent>()
                .HasOne(ms => ms.Student)
                .WithMany(m => m.Modules)
                .HasForeignKey(ms => ms.ModuleID);
            //student lecture selections
            modelBuilder.Entity<LectureStudent>()
                .HasKey(x => new { x.LectureID, x.StudentID });
            modelBuilder.Entity<LectureStudent>()
                .HasOne(ms => ms.Lecture)
                .WithMany(m => m.Students)
                .HasForeignKey(ms => ms.StudentID);
            modelBuilder.Entity<LectureStudent>()
                .HasOne(ms => ms.Student)
                .WithMany(m => m.Lectures)
                .HasForeignKey(ms => ms.LectureID);
            //informal gathering time tables
            modelBuilder.Entity<InformalGatheringTimeTable>()
                .HasKey(x => new { x.InformalGatheringID, x.TimeTableID });
            modelBuilder.Entity<InformalGatheringTimeTable>()
                .HasOne(ms => ms.TimeTable)
                .WithMany(m => m.InformalGatherings)
                .HasForeignKey(ms => ms.InformalGatheringID);
            modelBuilder.Entity<InformalGatheringTimeTable>()
                .HasOne(ms => ms.InformalGathering)
                .WithMany(m => m.TimeTables)
                .HasForeignKey(ms => ms.TimeTableID);
            //lecture time tables
            modelBuilder.Entity<LectureTimeTable>()
                .HasKey(x => new { x.LectureID, x.TimeTableID });
            modelBuilder.Entity<LectureTimeTable>()
                .HasOne(ms => ms.Lecture)
                .WithMany(m => m.TimeTables)
                .HasForeignKey(ms => ms.TimeTableID);
            modelBuilder.Entity<LectureTimeTable>()
                .HasOne(ms => ms.TimeTable)
                .WithMany(m => m.Lectures)
                .HasForeignKey(ms => ms.LectureID);
            //student gathering selections
            modelBuilder.Entity<InformalGatheringStudent>()
                .HasKey(x => new { x.InformalGatheringID, x.StudentID });
            modelBuilder.Entity<InformalGatheringStudent>()
                .HasOne(ms => ms.InformalGathering)
                .WithMany(m => m.Students)
                .HasForeignKey(ms => ms.StudentID);
            modelBuilder.Entity<InformalGatheringStudent>()
                .HasOne(ms => ms.Student)
                .WithMany(m => m.InformalGatherings)
                .HasForeignKey(ms => ms.InformalGatheringID);
        }

        
    }
}
