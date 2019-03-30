using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymManagement.Api.Controllers.DTO.Mailchimp
{
    public class Category
    {
        public string ListId { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public int DisplayOrder { get; set; }
        public string Type { get; set; }
        public List<Interest> Interests { get; set; }
    }
}
