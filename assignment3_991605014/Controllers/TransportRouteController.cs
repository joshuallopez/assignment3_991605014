using Microsoft.AspNetCore.Mvc;
using assignment3_991605014.Data;
using assignment3_991605014.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace assignment3_991605014.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportRouteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransportRouteController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TransportRoute
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransportRoute>>> GetRoutes()
        {
            return await _context.Routes.ToListAsync();
        }

        // GET: api/TransportRoute/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransportRoute>> GetRoute(int id)
        {
            var route = await _context.Routes.FindAsync(id);

            if (route == null)
            {
                return NotFound();
            }

            return route;
        }

        // POST: api/TransportRoute
        [HttpPost]
        public async Task<ActionResult<TransportRoute>> PostRoute(TransportRoute route)
        {
            _context.Routes.Add(route);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRoute), new { id = route.RouteId }, route);
        }

        // PUT: api/TransportRoute/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoute(int id, TransportRoute route)
        {
            if (id != route.RouteId)
            {
                return BadRequest();
            }

            _context.Entry(route).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RouteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/TransportRoute/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoute(int id)
        {
            var route = await _context.Routes.FindAsync(id);
            if (route == null)
            {
                return NotFound();
            }

            _context.Routes.Remove(route);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RouteExists(int id)
        {
            return _context.Routes.Any(e => e.RouteId == id);
        }
    }
}
