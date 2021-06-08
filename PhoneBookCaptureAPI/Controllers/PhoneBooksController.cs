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
    public class PhoneBooksController : ControllerBase
    {
        private readonly PhoneBookContext _context;

        public PhoneBooksController(PhoneBookContext context)
        {
            _context = context;
        }

        // GET: api/PhoneBooks/GetPhoneBook
        [HttpGet("GetPhoneBook")]
        public async Task<ActionResult<IEnumerable<PhoneBook>>> GetPhoneBook()
        {
            return await _context.PhoneBook.ToListAsync();
        }

        // GET: api/PhoneBooks/GetPhoneBookById/5
        [HttpGet("GetPhoneBookById/{id}")]
        public async Task<ActionResult<PhoneBook>> GetPhoneBook(int id)
        {
            var phoneBook = await _context.PhoneBook.FindAsync(id);

            if (phoneBook == null)
            {
                return NotFound();
            }

            return phoneBook;
        }

        // PUT: api/PhoneBooks/UpdatePhoneBook/5
        [HttpPut("UpdatePhoneBook/{id}")]
        public async Task<IActionResult> UpdatePhoneBook([FromRoute] int id, [FromBody] PhoneBook phoneBook)
        {
            if (id != phoneBook.PhoneBookID)
            {
                return BadRequest();
            }

            _context.Entry(phoneBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneBookExists(id))
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

        // POST: api/PhoneBooks/PostPhoneBook
        [HttpPost("PostPhoneBooks")]
        public async Task<ActionResult<PhoneBook>> PostPhoneBooks([FromBody] PhoneBook phoneBook)
        {
            _context.PhoneBook.Add(phoneBook);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhoneBook", new { id = phoneBook.PhoneBookID }, phoneBook);
        }

        private bool PhoneBookExists(int id)
        {
            return _context.PhoneBook.Any(e => e.PhoneBookID == id);
        }
    }
}
