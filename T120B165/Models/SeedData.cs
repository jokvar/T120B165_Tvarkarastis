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
                        Password = "dovydovy2"
                    },
                    new Student
                    {
                        FirstName = "Calvin",
                        LastName = "Berkhousen",
                        Username = "3picSexyCalvin",
                        Password = "calvin123"
                    }
                );
                context.SaveChanges();
            }
        }

    }
}
