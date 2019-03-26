using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using GymManagement.DataModel;
using GymManagement.Api.Context;
using GymManagement.Api.Config;

namespace GymManagement.Api.Controllers
{
    [Route("api/Mailchimp")]
    public class MailchimpController: ControllerBase
    {
        private readonly GymManagementDataContext _context;
        private readonly ApiSettings _settings;

        public MailchimpController(GymManagementDataContext context, IOptions<ApiSettings> settings) { _context = context; _settings = settings.Value; }

        [HttpGet("List")]
        public IActionResult GetLists()
        {
            return Ok();
        }

        [HttpPost("List")]
        public IActionResult CreateList()
        {
            return Ok();
        }

        [HttpGet("List/{listId}/members")]
        public IActionResult GetListMembers()
        {
            return Ok();
        }

        [HttpPost("List/{listId}/members")]
        public IActionResult AddListMembers()
        {
            return Ok();
        }
    }
}
