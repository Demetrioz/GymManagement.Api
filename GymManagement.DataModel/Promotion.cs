using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymManagement.DataModel
{
    [Table("Promotion")]
    public class Promotion
    {
        [Key]
        public int PromotionId { get; set; }
        public int? ContactId { get; set; }
        public int? TypeId { get; set; }
        public int? StatusId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Expiration { get; set; }
        public DateTime? Modified { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
