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
    [Route("api/Status")]
    public class StatusController : ControllerBase
    {
        private GymManagementDataContext _context;

        public StatusController(GymManagementDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetStatuses()
        {
            var statuses = _context.Statuses.ToList();
            return Ok(statuses);
        }
    }
}
