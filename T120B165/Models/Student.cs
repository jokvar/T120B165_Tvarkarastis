using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace T120B165.Models
{
    /// <summary>
    /// Test - this should appear in swagger around Student
    /// </summary>
    public class Student
    {
        public int ID { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z][a-z]*$")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z][a-z]*$")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]*$")]
        [StringLength(20, MinimumLength = 7)]
        public string Username { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]*$")]
        [StringLength(60, MinimumLength = 7)]
        public string Password { get; set; } //salted hash
        public string ApiKey { get; set; }
        //
        [Required]
        [StringLength(5, MinimumLength = 5)]
        [RegularExpression(@"^[A-Z][0-9][0-9][0-9][0-9]$")]
        public string Vidko { get; set; }
        //subscription
        public ICollection<ModuleStudent> Modules { get; set; }
        //events
        public ICollection<LectureStudent> Lectures { get; set; }
        public ICollection<InformalGatheringStudent> InformalGatherings { get; set; }
    }
}
