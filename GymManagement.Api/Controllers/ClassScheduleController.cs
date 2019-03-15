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
            var classSchedules = _context.ClassSchedules.Where(s => s.IsDeleted == false).ToList();
            return Ok(classSchedules);
        }

        // Post: /api/Class
        [HttpPost]
        public async Task<ActionResult<ClassSchedule>> CreateClassSchedule(ClassSchedule newClassSchedule)
        {
            // TODO: take an array and add multiple schedules
            
            // Set default values
            newClassSchedule.Created = DateTime.Now;
            newClassSchedule.IsDeleted = false;

            _context.ClassSchedules.Add(newClassSchedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClassSchedules), new { id = newClassSchedule.ClassScheduleId }, newClassSchedule);
        }

        // api/ClassSchedule/Id
        [HttpPost("{id}")]
        public async Task<ActionResult<ClassSchedule>> DeleteClassSchedule(int id)
        {
            var classSchedule = _context.ClassSchedules.Where(s => s.ClassScheduleId == id).FirstOrDefault();
            if (classSchedule != null)
            {
                classSchedule.IsDeleted = true;
                classSchedule.Modified = DateTime.Now;
                await _context.SaveChangesAsync();
                return Ok(true);
            }
            else
                return NotFound();

        }

        [HttpPatch]
        public async Task<ActionResult<ClassSchedule>> UpdateClassSchedules([FromBody]ClassSchedule[] classSchedules)
        {
            try
            {
                foreach (var schedule in classSchedules)
                {
                    schedule.Modified = DateTime.Now;
                    _context.ClassSchedules.Update(schedule);
                }
                await _context.SaveChangesAsync();

                return Ok(classSchedules);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
