using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using GymManagement.DataModel;
using GymManagement.Api.Context;

namespace GymManagement.Api.Controllers
{
    [Route("api/Source")]
    public class SourceController : ControllerBase
    {
        private GymManagementDataContext _context;

        public SourceController(GymManagementDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetSources()
        {
            var sources = _context.Sources.ToList();
            return Ok(sources);
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateSources([FromBody]Source[] sources)
        {
            foreach(var source in sources)
            {
                // Set default information
                source.Created = DateTime.Now;
                source.IsDeleted = false;

                _context.Sources.Add(source);
            }
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetSources), new { }, sources);
        }

        [HttpPatch]
        [Authorize]
        public IActionResult UpdateSources([FromBody]Source[] sources)
        {
            foreach(var source in sources)
            {
                source.Modified = DateTime.Now;
                _context.Sources.Update(source);
            }
            _context.SaveChanges();

            return Ok(sources);
        }
    }
}