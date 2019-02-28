using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymManagement.DataModel
{
    [Table("GymManagementLog")]
    public class GymManagementLog
    {
        [Key]
        public int LogId { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
