using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymManagement.Api.Controllers.DTO.Mailchimp
{
    public class Interest
    {
        public string category_id { get; set; }
        public string list_id { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public int subscriber_count { get; set; }
    }
}
