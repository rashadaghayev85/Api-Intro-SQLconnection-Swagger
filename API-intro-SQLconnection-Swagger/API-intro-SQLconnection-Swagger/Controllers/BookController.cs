using API_intro_SQLconnection_Swagger.Data;
using API_intro_SQLconnection_Swagger.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_intro_SQLconnection_Swagger.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDBContext _context;

        public BookController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Books.ToListAsync());
        }
        [HttpPost]

        public async Task<IActionResult> Create([FromBody] Book book)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Create", book);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int? id)
        {
            if (id == null) return BadRequest();
            var data = await _context.Books.FindAsync(id);
            if (data == null) return NotFound();
            _context.Books.Remove(data);
            await _context.SaveChangesAsync();
            return Ok(data);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] Book book)
        {
            if (!ModelState.IsValid) return BadRequest();
            var data = await _context.Books.FindAsync(id);
            if (data == null) return NotFound();
            data.Name = book.Name;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string? str)
        {
            return Ok(str == null ? await _context.Books.ToListAsync() : await _context.Books.Where(m => m.Name.Contains(str)).ToListAsync());
        }
    }
}
