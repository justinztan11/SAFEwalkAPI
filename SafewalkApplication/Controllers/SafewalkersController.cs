using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafewalkApplication.Models;

namespace SafewalkApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SafewalkersController : ControllerBase
    {
        private readonly SafewalkDatabaseContext _context;

        public SafewalkersController(SafewalkDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Safewalkers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Safewalker>>> GetSafewalker()
        {
            return await _context.Safewalker.ToListAsync();
        }

        // GET: api/Safewalkers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Safewalker>> GetSafewalker(string id)
        {
            var safewalker = await _context.Safewalker.FindAsync(id);

            if (safewalker == null)
            {
                return NotFound();
            }

            return safewalker;
        }

        // PUT: api/Safewalkers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSafewalker(string id, Safewalker safewalker)
        {
            if (id != safewalker.WalkerId)
            {
                return BadRequest();
            }

            _context.Entry(safewalker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SafewalkerExists(id))
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

        // POST: api/Safewalkers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Safewalker>> PostSafewalker(Safewalker safewalker)
        {
            _context.Safewalker.Add(safewalker);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SafewalkerExists(safewalker.WalkerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSafewalker", new { id = safewalker.WalkerId }, safewalker);
        }

        // DELETE: api/Safewalkers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Safewalker>> DeleteSafewalker(string id)
        {
            var safewalker = await _context.Safewalker.FindAsync(id);
            if (safewalker == null)
            {
                return NotFound();
            }

            _context.Safewalker.Remove(safewalker);
            await _context.SaveChangesAsync();

            return safewalker;
        }

        private bool SafewalkerExists(string id)
        {
            return _context.Safewalker.Any(e => e.WalkerId == id);
        }
    }
}
