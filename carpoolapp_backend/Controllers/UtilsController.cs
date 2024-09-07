using carpoolapp_backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace carpoolapp_backend.Controllers
{
    [ApiController]
    [Route("api")]
    public class UtilsController : Controller
    {
        private readonly DataContext _context;

        public UtilsController(DataContext context) { _context = context; }

        [HttpGet("locations")]
        public async Task<ActionResult<IEnumerable<string>>> GetLocationOptions()
        {
            var startLocations = await _context.Rides
                .Select(r => r.StartLocation)
                .Distinct()
                .ToListAsync();

            var endLocations = await _context.Rides
                .Select(r => r.EndLocation)
                .Distinct()
                .ToListAsync();

            var allLocations = startLocations.Concat(endLocations)
                .Distinct()
                .OrderBy(l => l)
                .ToList();

            return allLocations;
        }
    }
}
