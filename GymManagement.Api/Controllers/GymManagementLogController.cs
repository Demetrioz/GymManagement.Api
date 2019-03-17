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
    [Route("api/Log")]
    public class GymManagementLogController : ControllerBase
    {
        private readonly GymManagementDataContext _context;

        public GymManagementLogController(GymManagementDataContext context) { _context = context; }

        // Get: /api/Log
        // TODO: Add filter
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GymManagementLog>>> GetLogs(string filter = null)
        {
            return await _context.GymManagementLogs.ToListAsync();
        }

        // Get: /api/Log/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<GymManagementLog>> GetLog(int logId)
        {
            var log = await _context.GymManagementLogs.FindAsync(logId);
            return log;
        }

        // Post: /api/Log
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<GymManagementLog>> CreateLog(GymManagementLog log)
        {
            _context.GymManagementLogs.Add(log);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLog), new { id = log.LogId }, log);
        }
    }
}
