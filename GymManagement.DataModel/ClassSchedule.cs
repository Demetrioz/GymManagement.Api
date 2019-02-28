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
        public int? ScheduleTypeId { get; set; }
        public DateTime? ScheduleStart { get; set; }
        public DateTime? ScheduleStop { get; set; }
        public string ClassStart { get; set; }
        public string ClassStop { get; set; }
        public string Attendance { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
