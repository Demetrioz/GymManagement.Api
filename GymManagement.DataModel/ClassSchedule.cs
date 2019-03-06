using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymManagement.DataModel
{
    [Table("ClassSchedule")]
    public class ClassSchedule
    {
        [Key]
        public int ClassScheduleId { get; set; }
        public int? ClassTypeId { get; set; }
        public string Day { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? Duration { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
