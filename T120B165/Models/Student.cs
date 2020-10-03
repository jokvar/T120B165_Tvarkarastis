using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace T120B165.Models
{
    public class Student : User
    {
        [Required]
        [StringLength(5, MinimumLength = 5)]
        [RegularExpression(@"^[A-Z][0-9][0-9][0-9][0-9]$")]

        public string Vidko { get; set; }

        //public ICollection<ModuleStudent> ModuleStudents { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
