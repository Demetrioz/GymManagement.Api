using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymManagement.DataModel
{
    [Table("ClassAttendance")]
    public class ClassAttendance
    {
        [Key]
        public int ClassAttendanceId { get; set; }
        public int? ClassId { get; set; }
        public int? ContactId { get; set; }
        public bool? Present { get; set; }
    }
}
