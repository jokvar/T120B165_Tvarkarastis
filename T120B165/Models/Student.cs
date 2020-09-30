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
        public string Vidko { get; set; }
    }
}
