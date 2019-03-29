using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using GymManagement.DataModel;
using GymManagement.Api.Context;
using GymManagement.Api.Config;
using GymManagement.Api.Services;
using GymManagement.Api.Core;

namespace GymManagement.Api.Controllers
{
    [Route("api/Mailchimp")]
    public class MailchimpController: ControllerBase
    {
        private readonly GymManagementDataContext _context;
        private readonly ApiSettings _settings;

        public MailchimpController(GymManagementDataContext context, IOptions<ApiSettings> settings) { _context = context; _settings = settings.Value; }

        [HttpGet("List")]
        [Authorize]
        public IActionResult GetLists(string filter = null)
        {
            var service = new MailchimpService(_settings);
            var response = service.Read("/lists", filter);
            return Ok(response);
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
