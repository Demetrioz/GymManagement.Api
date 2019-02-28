using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymManagement.DataModel
{
    [Table("Location")]
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        public int? ParentLocationId { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public int? TypeId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
