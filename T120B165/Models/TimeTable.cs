using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace T120B165.Models
{
    public class TimeTable
    {
        public int ID { get; set; }
        public ICollection<InformalGatheringTimeTable> InformalGatherings { get; set; }
        public ICollection<LectureTimeTable> Lectures { get; set; }
        public int StudentID { get; set; }
        public virtual Student Student { get; set; }
    }
}
