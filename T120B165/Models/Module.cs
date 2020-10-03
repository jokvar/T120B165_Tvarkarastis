using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace T120B165.Models
{
    public class Module
    {
        public int ID { get; set; }
        [Required]
        [RegularExpression(@"^\b[A-Z].*?\b$")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z][0-9][0-9][0-9][A-Z][0-9][0-9][0-9]$")]
        public string Code { get; set; }
        public int LecturerID { get; set; }
        public virtual Lecturer Lecturer { get; set; }

        //public ICollection<ModuleStudent> ModuleStudents { get; set; }
        public ICollection<ModuleStudent> Students { get; set; }
    }

}
