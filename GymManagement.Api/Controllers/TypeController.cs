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
    [Route("api/Type")]
    public class TypeController : ControllerBase
    {
        private readonly GymManagementDataContext _context;

        public TypeController (GymManagementDataContext context) { _context = context; }

        // Get: /api/Type/{category}
        [HttpGet("{category}")]
        public async Task<ActionResult<IEnumerable<Class>>> GetTypesByCategory(string category)
        {
            var types = await _context.Types.Where(type => type.Category == category).ToListAsync();
            return Ok(types);
        }
    }
}
