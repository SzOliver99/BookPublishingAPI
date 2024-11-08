using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookPublishingAPI.Entities
{
    public class Publishing
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int BookId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime PublishDate { get; set; }

        public virtual Author Author { get; set; }
        public virtual Book Book { get; set; }
    }
}
