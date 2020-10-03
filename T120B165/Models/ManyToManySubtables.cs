using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace T120B165.Models
{
    public class ModuleStudent
    {
        public int? ModuleID { get; set; }
        public virtual Module Module { get; set; }

        public int? StudentID { get; set; }
        public virtual Student Student { get; set; }
    }
    public class LectureStudent
    {
        public int? LectureID { get; set; }
        public virtual Lecture Lecture { get; set; }

        public int? StudentID { get; set; }
        public virtual Student Student { get; set; }
    }

    public class InformalGatheringTimeTable
    {
        public int? InformalGatheringID { get; set; }
        public virtual InformalGathering InformalGathering { get; set; }
        public int? TimeTableID { get; set; }
        public virtual TimeTable TimeTable { get; set; }
    }

    public class LectureTimeTable
    {
        public int? LectureID { get; set; }
        public virtual Lecture Lecture { get; set; }
        public int? TimeTableID { get; set; }
        public virtual TimeTable TimeTable { get; set; }
    }

    public class InformalGatheringStudent
    {
        public int? InformalGatheringID { get; set; }
        public virtual InformalGathering InformalGathering { get; set; }
        public int? StudentID { get; set; }
        public virtual Student Student { get; set; }
    }

    
}

