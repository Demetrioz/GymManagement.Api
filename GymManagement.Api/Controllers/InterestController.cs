using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using GymManagement.DataModel;
using GymManagement.Api.Context;
using GymManagement.Api.Config;

namespace GymManagement.Api.Controllers
{
    [Route("api/Interest")]
    public class InterestController : Controller
    {
        private GymManagementDataContext _context;

        public InterestController(GymManagementDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetInterests()
        {
            var interests = _context.Interests.ToList();
            return Ok(interests);
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateInterest([FromBody]Interest[] interests)
        {
            foreach (var interest in interests)
            {
                // Set default information
                
                interest.Created = DateTime.Now;
                interest.IsDeleted = false;

                _context.Interests.Add(interest);
            }
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetInterests), new { }, interests);
        }

        [HttpPatch]
        [Authorize]
        public IActionResult UpdateInterest([FromBody]Interest[] interests)
        {
            foreach(var interest in interests)
            {
                interest.Modified = DateTime.Now;
                _context.Interests.Update(interest);
            }
            _context.SaveChanges();

            return Ok(interests);
        }
    }
}
