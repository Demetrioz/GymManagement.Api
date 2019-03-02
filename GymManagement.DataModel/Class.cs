using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymManagement.DataModel
{
    [Table("Class")]
    public class Class
    {
        [Key]
        public int ClassId { get; set; }
        public int? TypeId { get; set; }
        public int? LocationId { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? Stop { get; set; }
        public string Attendance { get; set; }
        public int? MaxAttendance { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
