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
    }
}
