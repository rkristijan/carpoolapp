using carpoolapp_backend.Data;
using carpoolapp_backend.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace carpoolapp_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RidesController : Controller {
        private readonly DataContext _context;

        public RidesController(DataContext context) { _context = context; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ride>>> GetRides()
        {
            return await _context.Rides.ToListAsync();
        }

        [HttpPost("add")]
        public async Task<ActionResult<Ride>> AddRide(Ride ride)
        {
            ride.CreationDate = DateTime.Now;

            _context.Rides.Add(ride);
            await _context.SaveChangesAsync(); ;

            return CreatedAtAction(nameof(GetRides), new { id = ride.Id }, ride);
        }

        [HttpPut("{id}/book")]
        public async Task<IActionResult> BookSeat(int id)
        {
            var ride = await _context.Rides.FindAsync(id);

            if (ride != null)
            {
                if (ride.AvailableSeats == 0)
                {
                    return BadRequest();
                }
                else
                {
                    ride.AvailableSeats--;
                }
            }
            else
            {
                return NotFound();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RideExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(ride);
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelSeat(int id)
        {
            var ride = await _context.Rides.FindAsync(id);

            if (ride != null)
            {
                if (ride.AvailableSeats == ride.VehicleSeats)
                {
                    return BadRequest();
                }
                else
                {
                    ride.AvailableSeats++;
                }
            }
            else
            {
                return NotFound();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RideExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(ride);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRide(int id)
        {
            var ride = await _context.Rides.FindAsync(id);
            if (ride == null)
            {
                return NotFound();
            }

            _context.Rides.Remove(ride);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RideExists(int id)
        {
            return _context.Rides.Any(e => e.Id == id);
        }
    }


}
