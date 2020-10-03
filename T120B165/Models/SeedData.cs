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
                foreach (var lecturer in context.Lecturers)
                {                 
                    context.Lecturers.Remove(lecturer);
                }
                foreach (var student in context.Students)
                {
                    context.Students.Remove(student);
                }
                foreach (var module in context.Modules)
                {
                    context.Modules.Remove(module);
                }
                context.SaveChanges();
                //Check if objects exist
                if (context.Modules.Any() || context.Students.Any() || context.Lecturers.Any())
                {
                    return; //DB has been seeded
                }
                context.Lecturers.AddRange(
                    new Lecturer
                    {
                        FirstName = "Lecturer",
                        LastName = "Lecturerhausen",
                        Username = "lecturer1",
                        Password = "password1"
                    },
                    new Lecturer
                    {
                        FirstName = "John",
                        LastName = "Lecturerstein",
                        Username = "lecturer2",
                        Password = "password2"
                    }
                );
                context.Students.AddRange(
                    new Student
                    {
                        FirstName = "David",
                        LastName = "Yanusevich",
                        Username = "dovydovy",
                        Password = "dovydovy2",
                        Vidko = "C1008"
                    },
                    new Student
                    {
                        FirstName = "Calvin",
                        LastName = "Berkhousen",
                        Username = "3picSexyCalvin",
                        Password = "calvin123",
                        Vidko = "Z2009"
                    }
                );
                context.SaveChanges();
                var query = from l in context.Lecturers
                            orderby l.FirstName
                            select l;
                var lecturers = query.ToList();
                System.Diagnostics.Debug.WriteLine(lecturers.Count);
                context.Modules.AddRange(
                    new Module
                    {
                        Code = "A333A333",
                        Lecturer = lecturers[0],
                        Name = "Website Designe"
                    },
                    new Module
                    {
                        Code = "B444B444",
                        Lecturer = context.Lecturers.Single(l => l.FirstName == "John"),
                        Name = "Plantt Growing"
                    }
                );
                context.SaveChanges();
                context.ModuleStudents.Add(
                    new ModuleStudent
                    {
                        Student = context.Students.Single(s => s.FirstName == "Calvin"),
                        Module = context.Modules.Single(m => m.Name == "Website Designe")
                    }
                    );
                context.SaveChanges();
            }
        }

    }
}
