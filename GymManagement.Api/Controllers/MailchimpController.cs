using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using GymManagement.DataModel;
using GymManagement.Api.Context;
using GymManagement.Api.Config;
using GymManagement.Api.Services;
using GymManagement.Api.Core;
using GymManagement.Api.Controllers.DTO.Mailchimp;

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

        [HttpGet("List/{listId}/Groups")]
        [Authorize]
        public IActionResult GetGroups(string listId, string filter = null)
        {
            var service = new MailchimpService(_settings);

            // Get the groups
            var response = service.Read($"/lists/{listId}/interest-categories", filter);

            // Turn the group into an object 
            var responseObject = (ApiResponse)response;
            var dataString = JsonConvert.SerializeObject(responseObject.Data);

            //iterate through the categories and grab the category interests
            var list = (string)JObject.Parse(dataString)["list_id"];
            var itemTotal = (int)JObject.Parse(dataString)["total_items"];
            var categories = JObject.Parse(dataString)["categories"].ToObject<List<Category>>();
            
            foreach(var category in categories)
            {
                // Set the list id since the variable names don't match
                category.ListId = list;

                // get the interests for the group
                var interestResponse = service.Read($"/lists/{list}/interest-categories/{category.Id}/interests", null);

                var interestResponseObject = (ApiResponse)interestResponse;
                var interestDataString = JsonConvert.SerializeObject(interestResponseObject.Data);
                var interests = JObject.Parse(interestDataString)["interests"].ToObject<List<DTO.Mailchimp.Interest>>();

                category.Interests = interests;
            }

            var group = new Group
            {
                ListId = list,
                Categories = categories,
                TotalItems = itemTotal
            };

            return Ok(group);
        }
    }
}
