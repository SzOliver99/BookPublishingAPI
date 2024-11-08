using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookPublishingAPI.Context;
using BookPublishingAPI.Entities;

namespace BookPublishingAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(BookContext context) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Book book)
        {
            if (book is null) return BadRequest("Empty input");

            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var books = await context.Books.ToListAsync();
            if (books is null) return NotFound("No books found");

            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await context.Books.FirstOrDefaultAsync(c => c.Id == id);
            if (book is null) return NotFound("No book found");

            return Ok(book);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] Book book)
        {
            var bookToUpdate = await context.Books.FirstOrDefaultAsync(c => c.Id == id);
            if (bookToUpdate == null) return NotFound("Book not found");

            bookToUpdate.Title = book.Title;
            bookToUpdate.Genre = book.Genre;

            await context.SaveChangesAsync();
            return Ok("Updated successfuly");
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await context.Books.FirstOrDefaultAsync(c => c.Id == id);
            if (book == null) return NotFound("Book not found");

            context.Books.Remove(book);
            await context.SaveChangesAsync();
            return Ok("Deleted successfuly");
        }
    }
}