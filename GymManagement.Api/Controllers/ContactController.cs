using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymManagement.DataModel;
using GymManagement.Api.Context;


namespace GymManagement.Api.Controllers
{
    [Route("api/Contact")]
    [ApiController]
    public class ContactController : ControllerBase
    {

        private GymManagementDataContext _context;

        public ContactController(GymManagementDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetContacts()
        {
            var contacts = _context.Contacts.Include("Status").ToList();
            return Ok(contacts);
        }

        [HttpPost]
        public IActionResult CreateContact(Contact[] contacts)
        {
            foreach(var contact in contacts)
            {
                // Set default information
                contact.TimesContacted = 1;
                contact.Converted = false;
                contact.Created = DateTime.Now;
                contact.IsDeleted = false;


                _context.Contacts.Add(contact);
            }
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetContacts), new { }, contacts);
        }

        [HttpPatch]
        public async Task<ActionResult<Contact>> UpdateContacts([FromBody]Contact[] contacts)
        {
            try
            {
                var prospect = _context.Statuses.Where(s => s.Name == "prospect").FirstOrDefault();

                foreach (var contact in contacts)
                {
                    if (contact.StatusId != prospect.StatusId)
                    {
                        contact.Converted = true;
                        contact.DateConverted = DateTime.Now;
                    }
                    else
                        contact.Converted = false;

                    contact.Modified = DateTime.Now;
                    _context.Contacts.Update(contact);
                }
                await _context.SaveChangesAsync();

                return Ok(contacts);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
