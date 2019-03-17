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
    [Route("api/Type")]
    public class TypeController : ControllerBase
    {
        private readonly GymManagementDataContext _context;

        public TypeController (GymManagementDataContext context) { _context = context; }

        [HttpGet]
        [Authorize]
        public IActionResult GetTypes()
        {
            var types = _context.Types.Where(t => t.IsDeleted == false).ToList();
            return Ok(types);
        } 

        // Get: /api/Type/{category}
        [HttpGet("{category}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Class>>> GetTypesByCategory(string category)
        {
            var types = await _context.Types.Where(type => type.Category == category).ToListAsync();
            return Ok(types);
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateType([FromBody]DataModel.Type[] types)
        {
            foreach(var type in types)
            {
                type.Created = DateTime.Now;
                type.IsDeleted = false;

                _context.Types.Add(type);
            }
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTypes), new { }, types);
        }

        [HttpPatch]
        [Authorize]
        public async Task<ActionResult<DataModel.Type>> UpdateTypes([FromBody]DataModel.Type[] types)
        {
            try
            {
                foreach(var type in types)
                {
                    type.Modified = DateTime.Now;
                    _context.Types.Update(type);
                }
                await _context.SaveChangesAsync();

                return Ok(types);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
