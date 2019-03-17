using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymManagement.DataModel
{
    [Table("UserRoleClaimMap")]
    public class UserRoleClaimMap
    {
        [Key]
        public int UserRoleClaimMapId { get; set; }
        public int? RoleId { get; set; }
        public int? ClaimId { get; set; }
    }
}
