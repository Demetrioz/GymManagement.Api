using System;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
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
using GymManagement.Api.Controllers.DTO.Requests;

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
        public IActionResult GetListMembers(string listId, string filter = null)
        {
            var service = new MailchimpService(_settings);
            var response = service.Read($"/lists/{listId}/members", filter);
            return Ok(response);
        }

        [HttpPost("List/{listId}/Members")]
        [Authorize]
        public IActionResult AddListMembers([FromBody]MailchimpListMember member, string listId)
        {
            var service = new MailchimpService(_settings);

            if(member.Interest != null)
            {
                var interests = service.Read($"/lists/{listId}/interest-categories", null);
                var interestObject = (ApiResponse)interests;
                var dataString = JsonConvert.SerializeObject(interestObject.Data);
                var categories = JObject.Parse(dataString)["categories"].ToObject<List<Category>>();

                foreach (var category in categories)
                {
                    if (category.Title == "Interest")
                    {
                        var groups = service.Read($"/lists/{listId}/interest-categories/{category.Id}/interests", null);

                        var groupResponseObject = (ApiResponse)groups;
                        var groupString = JsonConvert.SerializeObject(groupResponseObject.Data);
                        var groupInterests = JObject.Parse(groupString)["interests"].ToObject<List<DTO.Mailchimp.Interest>>();

                        var selectedInterest = groupInterests.Where(i => i.name == member.Interest).FirstOrDefault();
                        var memberInterests = new ExpandoObject();
                        memberInterests.TryAdd(selectedInterest.id, true);

                        var memberObject = new
                        {
                            email_address = member.Email,
                            status = "subscribed",
                            merge_fields = new
                            {
                                FNAME = member.FirstName,
                                LNAME = member.LastName,
                            },
                            interests = memberInterests,
                            timestamp_signup = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"),
                            tags = member.Tags,
                        };

                        var response = service.Create($"/lists/{listId}/members", memberObject);

                        return Ok(response);
                    }
                }

                return Ok("Provided Interest not found. No contact Added");
            }
            else
            {
                var memberObject = new
                {
                    email_address = member.Email,
                    status = "subscribed",
                    timestamp_signup = DateTime.Now.ToString(),
                    tags = member.Tags.ToArray(),
                };

                var response = service.Create($"/lists/{listId}/members", memberObject);

                return Ok(response);
            }
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

        [HttpGet("List/{listId}/Segments")]
        [Authorize]
        public IActionResult GetSegments(string listId, string filter = null)
        {
            var service = new MailchimpService(_settings);
            var response = service.Read($"/lists/{listId}/segments", filter);
            return Ok(response);
        }
    }
}
