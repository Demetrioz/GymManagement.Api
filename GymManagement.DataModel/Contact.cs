using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymManagement.DataModel
{
    [Table("Contact")]
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }
        public int? StatusId { get; set; }
        [ForeignKey("StatusId")]
        public Status Status { get; set; }
        public int? SourceId { get; set; }
        [ForeignKey("SourceId")]
        public Source Source { get; set; }
        public int? InterestId { get; set; }
        [ForeignKey("InterestId")]
        public Interest Interest { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? LastContact { get; set; }
        public int? TimesContacted { get; set; }
        public bool? Converted { get; set; }
        public DateTime? DateConverted { get; set; }
        public string LeadNotes { get; set; }
        public DateTime? NextAppointment { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
