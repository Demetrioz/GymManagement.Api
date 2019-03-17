using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymManagement.DataModel
{
    [Table("Claims")]
    public class Claims
    {
        [Key]
        public int ClaimId { get; set; }
        public string Name { get; set; }
        public bool? C { get; set; }
        public bool? R { get; set; }
        public bool? U { get; set; }
        public bool? D { get; set; }
    }
}
