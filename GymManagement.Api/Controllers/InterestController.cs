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
    }
}
