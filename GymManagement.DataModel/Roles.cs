using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymManagement.DataModel
{
    [Table("Roles")]
    public class Roles
    {
        [Key]
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Descritpion { get; set; }
    }
}
