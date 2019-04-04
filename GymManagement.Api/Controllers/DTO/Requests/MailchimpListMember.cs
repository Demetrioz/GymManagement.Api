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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] Tags { get; set; }
    }
}
