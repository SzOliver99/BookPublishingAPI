using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookPublishingAPI.Context;
using BookPublishingAPI.Entities;
using BookPublishingAPI.DTO;

namespace BookPublishingAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishingController(BookContext context) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PublishingDTO publishingDto)
        {
            if (publishingDto is null) return BadRequest("Empty input");
            var publishing = new Publishing
            {
                AuthorId = publishingDto.AuthorId,
                BookId = publishingDto.BookId,
                Name = publishingDto.Name,
                Country = publishingDto.Country,
                PublishDate = publishingDto.PublishDate,
            };

            await context.Publishings.AddAsync(publishing);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = publishing.Id }, publishing);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var publishings = await context.Publishings.Include(a => a.Author).Include(b => b.Book).ToListAsync();
            if (publishings is null) return NotFound("No publishings found");

            return Ok(publishings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var publishing = await context.Publishings.FirstOrDefaultAsync(c => c.Id == id);
            if (publishing is null) return NotFound("No publishing found");

            return Ok(publishing);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] Publishing publishing)
        {
            var publishingToUpdate = await context.Publishings.FirstOrDefaultAsync(c => c.Id == id);
            if (publishingToUpdate == null) return NotFound("Publishing not found");

            publishingToUpdate.Name = publishing.Name;
            publishingToUpdate.Country = publishing.Country;
            publishingToUpdate.PublishDate = publishing.PublishDate;

            await context.SaveChangesAsync();
            return Ok("Updated successfuly");
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var publishing = await context.Publishings.FirstOrDefaultAsync(c => c.Id == id);
            if (publishing == null) return NotFound("Publishing not found");

            context.Publishings.Remove(publishing);
            await context.SaveChangesAsync();
            return Ok("Deleted successfuly");
        }
    }
}