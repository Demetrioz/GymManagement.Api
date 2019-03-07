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
    [Route("api/ClassSchedule")]
    [ApiController]
    public class ClassScheduleController : ControllerBase
    {
        private readonly GymManagementDataContext _context;

        public ClassScheduleController(GymManagementDataContext context) { _context = context; }

        // Get: /api/Class
        // TODO: Add filter
        [HttpGet]
        public IActionResult GetClassSchedules()
        {
            var classSchedules = _context.ClassSechedules.ToList();
            return Ok(classSchedules);
        }

        // Post: /api/Class
        [HttpPost]
        public async Task<ActionResult<Class>> CreateClassSchedule(ClassSchedule newClassSchedule)
        {
            // TODO: take an array and add multiple schedules
            
            // Set default values
            newClassSchedule.Created = DateTime.Now;
            newClassSchedule.IsDeleted = false;

            _context.ClassSechedules.Add(newClassSchedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClassSchedules), new { id = newClassSchedule.ClassScheduleId }, newClassSchedule);
        }
    }
}
