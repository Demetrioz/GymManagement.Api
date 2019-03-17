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
    }
}