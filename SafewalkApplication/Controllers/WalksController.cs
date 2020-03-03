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
    public class WalksController : ControllerBase
    {
        private readonly SafewalkDatabaseContext _context;

        public WalksController(SafewalkDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Walks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Walk>>> GetWalk()
        {
            return await _context.Walk.ToListAsync();
        }

        // GET: api/Walks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Walk>> GetWalk(string id)
        {
            var walk = await _context.Walk.FindAsync(id);

            if (walk == null)
            {
                return NotFound();
            }

            return walk;
        }

        // PUT: api/Walks/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWalk(string id, Walk walk)
        {
            if (id != walk.WalkId)
            {
                return BadRequest();
            }

            _context.Entry(walk).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WalkExists(id))
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

        // POST: api/Walks
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Walk>> PostWalk(Walk walk)
        {
            _context.Walk.Add(walk);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (WalkExists(walk.WalkId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetWalk", new { id = walk.WalkId }, walk);
        }

        // DELETE: api/Walks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Walk>> DeleteWalk(string id)
        {
            var walk = await _context.Walk.FindAsync(id);
            if (walk == null)
            {
                return NotFound();
            }

            _context.Walk.Remove(walk);
            await _context.SaveChangesAsync();

            return walk;
        }

        private bool WalkExists(string id)
        {
            return _context.Walk.Any(e => e.WalkId == id);
        }
    }
}
