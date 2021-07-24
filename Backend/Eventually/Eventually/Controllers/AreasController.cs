using Eventually.DTOs;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventually.Controllers
{
    [ApiController]
    [Route("api/areas")]
    public class AreasController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AreasController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AreaDTO>>> Get()
        {
            var areas = await _applicationDbContext.Areas.ToListAsync();
            return areas.Select(area => new AreaDTO
            {
                Id = area.Id,
                Name = area.Name

            }).ToList();
        }

    }
}
