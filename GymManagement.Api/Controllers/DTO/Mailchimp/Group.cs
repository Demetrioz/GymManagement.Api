using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymManagement.Api.Controllers.DTO.Mailchimp
{
    public class Group
    {
        public string ListId { get; set; }
        public List<Category> Categories { get; set; }
        public int TotalItems { get; set; }
    }
}
