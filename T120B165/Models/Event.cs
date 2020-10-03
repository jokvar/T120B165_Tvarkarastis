using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace T120B165.Models
{
    public class Event
    {
        public int ID { get; set; }
        [DataType(DataType.DateTime), Required]
        public DateTime StartDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan Duration { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string Tags { get; set; }
    }

    public class InformalGathering : Event
    {
        public ICollection<InformalGatheringStudent> Students { get; set; }
        public ICollection<InformalGatheringTimeTable> TimeTables { get; set; }
    }

    public class Lecture : Event
    {
        public string Hall { get; set; }
        public ICollection<LectureStudent> Students { get; set; }
        public int? ModuleID { get; set; }
        public virtual Module Module { get; set; }
        public ICollection<LectureTimeTable> TimeTables { get; set; }
    }
}
