using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace T120B165.Models
{
    public class ModuleStudent
    {
        public int ModuleID { get; set; }
        public virtual Module Module { get; set; }

        public int StudentID { get; set; }
        public virtual Student Student { get; set;}
    }
}
