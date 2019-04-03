using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymManagement.Api.Controllers.DTO.Requests
{
    public class MailchimpListMember
    {
        public string Email { get; set; }
        public string Interest { get; set; }
        public List<string> Tags { get; set; }
    }
}
