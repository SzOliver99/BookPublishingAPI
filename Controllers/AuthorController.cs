using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookPublishingAPI.Context;
using BookPublishingAPI.Entities;

namespace BookPublishingAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController(BookContext context) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Author author)
        {
            if (author is null) return BadRequest("Empty input");

            await context.Authors.AddAsync(author);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = author.Id }, author);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var authors = await context.Authors.ToListAsync();
            if (authors is null) return NotFound("No authors found");

            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await context.Authors.FirstOrDefaultAsync(c => c.Id == id);
            if (author is null) return NotFound("No author found");

            return Ok(author);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] Author author)
        {
            var authorToUpdate = await context.Authors.FirstOrDefaultAsync(c => c.Id == id);
            if (authorToUpdate == null) return NotFound("Author not found");

            authorToUpdate.Name = author.Name;
            authorToUpdate.BirthDate = author.BirthDate;
            authorToUpdate.Nationality = author.Nationality;

            await context.SaveChangesAsync();
            return Ok("Updated successfuly");
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await context.Authors.FirstOrDefaultAsync(c => c.Id == id);
            if (author == null) return NotFound("Author not found");

            context.Authors.Remove(author);
            await context.SaveChangesAsync();
            return Ok("Deleted successfuly");
        }
    }
}