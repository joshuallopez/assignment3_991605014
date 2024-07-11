using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using assignment3_991605014.Data;
using assignment3_991605014.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment3_991605014.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TruckWorkshopController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TruckWorkshopController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TruckWorkshop
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TruckWorkshop>>> GetTruckWorkshops()
        {
            return await _context.TruckWorkshops.ToListAsync();
        }

        // GET: api/TruckWorkshop/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TruckWorkshop>> GetTruckWorkshop(int id)
        {
            var truckWorkshop = await _context.TruckWorkshops.FindAsync(id);

            if (truckWorkshop == null)
            {
                return NotFound();
            }

            return truckWorkshop;
        }

        // POST: api/TruckWorkshop
        [HttpPost]
        public async Task<ActionResult<TruckWorkshop>> PostTruckWorkshop(TruckWorkshop truckWorkshop)
        {
            _context.TruckWorkshops.Add(truckWorkshop);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTruckWorkshop", new { id = truckWorkshop.WorkOrderID }, truckWorkshop);
        }

        // PUT: api/TruckWorkshop/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTruckWorkshop(int id, TruckWorkshop truckWorkshop)
        {
            if (id != truckWorkshop.WorkOrderID)
            {
                return BadRequest();
            }

            _context.Entry(truckWorkshop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TruckWorkshopExists(id))
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

        // DELETE: api/TruckWorkshop/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTruckWorkshop(int id)
        {
            var truckWorkshop = await _context.TruckWorkshops.FindAsync(id);
            if (truckWorkshop == null)
            {
                return NotFound();
            }

            _context.TruckWorkshops.Remove(truckWorkshop);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TruckWorkshopExists(int id)
        {
            return _context.TruckWorkshops.Any(e => e.WorkOrderID == id);
        }
    }
}
