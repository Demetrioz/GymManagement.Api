using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymManagement.DataModel
{
    [Table("User")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public int? RoleId { get; set; }
        public int? ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool? ForcePasswordReset { get; set; }
        public DateTime? LastLogon { get; set; }
        public DateTime? PasswordExpiration { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
