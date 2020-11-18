using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using T120B165.Data;

namespace T120B165.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (T120B165Context context = new T120B165Context(serviceProvider.GetRequiredService<DbContextOptions<T120B165Context>>()))
            {
                if (!context.Students.Where(s => s.Username == "studentas").Any())
                {
                    context.Students.Add(new Student()
                    {
                        FirstName = "Studentas",
                        LastName = "Studentauskas",
                        Username = "studentas",
                        ApiKey = null,
                        Password = "this is never used",
                        Vidko = "Z4200"
                    });
                    context.SaveChanges();
                }
                if (!context.Students.Where(s => s.Username == "antras22").Any())
                {
                    context.Students.Add(new Student()
                    {
                        FirstName = "Antras",
                        LastName = "Antrauskas",
                        Username = "antras22",
                        ApiKey = null,
                        Password = "this is never used",
                        Vidko = "A2222"
                    });
                    context.SaveChanges();
                }
                if (context.Lectures.Any())
                {
                    return;
                }
                context.Lecturers.AddRange(new Lecturer[]
                {
                    new Lecturer()
                    {
                        ApiKey = "not used",
                        FirstName = "Jonas",
                        LastName = "Dėstytojauskas",
                        Password = "Unused",
                        Username = "jonasaaaa"
                    },
                    new Lecturer()
                    {
                        ApiKey = "not used",
                        FirstName = "Petras",
                        LastName = "Pavardauskas",
                        Password = "Unused",
                        Username = "petrasaaa"
                    }
                });
                context.SaveChanges();
                context.Modules.AddRange(new Module[]
                {
                    new Module()
                    {
                        Name = "Computer Gaming",
                        Code = "C111G123",
                        LecturerID = context.Lecturers.Where(l => l.FirstName == "Jonas").FirstOrDefault().ID
                    },
                    new Module()
                    {
                        Name = "Intense Sleeping",
                        Code = "I000S000",
                        LecturerID = context.Lecturers.Where(l => l.FirstName == "Petras").FirstOrDefault().ID
                    }
                });
                context.SaveChanges();
                context.Lectures.AddRange(new Lecture[]
                {
                    new Lecture()
                    {
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddHours(1),
                        Duration = TimeSpan.FromHours(1),
                        Name = "Intro to Gaming",
                        Address = "Gatve 15",
                        Description = "https://paypal.me/cash4cash",
                        Hall = "101",
                        ModuleID = context.Modules.Where(m => m.Code == "C111G123").FirstOrDefault().ID
                    },
                    new Lecture()
                    {
                        StartDate = DateTime.Now.AddDays(1),
                        EndDate = DateTime.Now.AddDays(1).AddHours(1),
                        Duration = TimeSpan.FromHours(1),
                        Name = "History of Video Games",
                        Address = "Gatve 15",
                        Description = "short history of snake and tetris.",
                        Hall = "208",
                        ModuleID = context.Modules.Where(m => m.Code == "C111G123").FirstOrDefault().ID
                    },
                    new Lecture()
                    {
                        StartDate = DateTime.Now.AddHours(12),
                        EndDate = DateTime.Now.AddHours(13),
                        Duration = TimeSpan.FromHours(1),
                        Name = "How to be a Gamer",
                        Address = "Gatve 15",
                        Description = "Self-help guide and tutorial",
                        Hall = "101",
                        ModuleID = context.Modules.Where(m => m.Code == "C111G123").FirstOrDefault().ID
                    },
                    new Lecture()
                    {
                        StartDate = DateTime.Now.AddHours(12).AddMinutes(30),
                        EndDate = DateTime.Now.AddHours(13).AddMinutes(30),
                        Duration = TimeSpan.FromHours(1),
                        Name = "Sleeping 101",
                        Address = "Skveras",
                        Description = "How to ZZZ and do it right",
                        Hall = "103",
                        ModuleID = context.Modules.Where(m => m.Code == "I000S000").FirstOrDefault().ID
                    }
                    ,
                    new Lecture()
                    {
                        StartDate = DateTime.Now.AddDays(1).AddMinutes(30),
                        EndDate = DateTime.Now.AddDays(1).AddMinutes(150),
                        Duration = TimeSpan.FromHours(2),
                        Name = "REM sleep",
                        Address = "Your home",
                        Description = "its Rapid Eye Movement - ok lecture over",
                        Hall = "your room",
                        ModuleID = context.Modules.Where(m => m.Code == "I000S000").FirstOrDefault().ID
                    }
                });
                context.SaveChanges();

                //foreach (var lecturer in context.Lecturers)
                //{                 
                //    context.Lecturers.Remove(lecturer);
                //}
                //foreach (var student in context.Students)
                //{
                //    context.Students.Remove(student);
                //}
                //foreach (var module in context.Modules)
                //{
                //    context.Modules.Remove(module);
                //}
                //context.SaveChanges();
                ////Check if objects exist
                //if (context.Modules.Any() || context.Students.Any() || context.Lecturers.Any())
                //{
                //    return; //DB has been seeded
                //}
                //context.Lecturers.AddRange(
                //    new Lecturer
                //    {
                //        FirstName = "Lecturer",
                //        LastName = "Lecturerhausen",
                //        Username = "lecturer1",
                //        Password = "password1"
                //    },
                //    new Lecturer
                //    {
                //        FirstName = "John",
                //        LastName = "Lecturerstein",
                //        Username = "lecturer2",
                //        Password = "password2"
                //    }
                //);
                //context.Students.AddRange(
                //    new Student
                //    {
                //        FirstName = "David",
                //        LastName = "Yanusevich",
                //        Username = "dovydovy",
                //        Password = "dovydovy2",
                //        Vidko = "C1008"
                //    },
                //    new Student
                //    {
                //        FirstName = "Calvin",
                //        LastName = "Berkhousen",
                //        Username = "3picSexyCalvin",
                //        Password = "calvin123",
                //        Vidko = "Z2009"
                //    }
                //);
                //context.SaveChanges();
                //var query = from l in context.Lecturers
                //            orderby l.FirstName
                //            select l;
                //var lecturers = query.ToList();
                //System.Diagnostics.Debug.WriteLine(lecturers.Count);
                //context.Modules.AddRange(
                //    new Module
                //    {
                //        Code = "A333A333",
                //        Lecturer = lecturers[0],
                //        Name = "Website Designe"
                //    },
                //    new Module
                //    {
                //        Code = "B444B444",
                //        Lecturer = context.Lecturers.Single(l => l.FirstName == "John"),
                //        Name = "Plantt Growing"
                //    }
                //);
                //context.SaveChanges();
                //context.ModuleStudents.Add(
                //    new ModuleStudent
                //    {
                //        Student = context.Students.Single(s => s.FirstName == "Calvin"),
                //        Module = context.Modules.Single(m => m.Name == "Website Designe")
                //    }
                //    );
                //context.SaveChanges();
            }
        }

    }
}
