using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace T120B165.Models
{
    public class User
    {
        public int Id { get; set; }
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
    }
}
