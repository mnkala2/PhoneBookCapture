using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBookCaptureAPI.Data;
using PhoneBookCaptureAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookCaptureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntriesController : ControllerBase
    {
        private readonly PhoneBookContext _context;

        public EntriesController(PhoneBookContext context)
        {
            _context = context;
        }

        // GET: api/Entries/GetEntries
        [HttpGet("GetEntries")]
        public async Task<ActionResult<IEnumerable<Entry>>> GetEntries()
        {
            return await _context.Entries.ToListAsync();
        }
        // GET: api/Entries/GetEntry/5
        [HttpGet("GetEntry/{id}")]
        public async Task<ActionResult<Entry>> GetEntry(int id)
        {
            var entry = await _context.Entries.FindAsync(id);

            if (entry == null)
            {
                return NotFound();
            }

            return entry;
        }

        // PUT: api/Entries/UpdateEntry/5       
        [HttpPut("UpdateEntry/{id}")]
        public async Task<IActionResult> UpdateEntry([FromRoute] int id, [FromBody] Entry entry)
        {
            if (id != entry.EntryID)
            {
                return BadRequest();
            }

            _context.Entry(entry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntryExists(id))
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

        // POST: api/Entries/PostEntry       
        [HttpPost("PostEntry")]
        public async Task<ActionResult<Entry>> PostEntry([FromBody] Entry entry)
        {
            _context.Entries.Add(entry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntry", new { id = entry.EntryID }, entry);
        }


        private bool EntryExists(int id)
        {
            return _context.Entries.Any(e => e.EntryID == id);
        }
    }
}
