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
    [Route("api/Class")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly GymManagementDataContext _context;

        public ClassController(GymManagementDataContext context) { _context = context; }

        // Get: /api/Class
        // TODO: Add filter
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Class>>> GetClasses(string filter = null)
        {
            return await _context.Classes.ToListAsync();
        }

        // Get: /api/Class/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Class>> GetClass(int classId)
        {
            var item = await _context.Classes.FindAsync(classId);
            return item;
        }

        // Post: /api/Class
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Class>> CreateClass(Class newClass)
        {
            _context.Classes.Add(newClass);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClass), new { id = newClass.ClassId }, newClass);
        }

    }
}