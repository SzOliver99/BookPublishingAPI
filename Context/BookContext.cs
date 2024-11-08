using BookPublishingAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookPublishingAPI.Context
{
    public class BookContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publishing> Publishings { get; set; }
    }
}
