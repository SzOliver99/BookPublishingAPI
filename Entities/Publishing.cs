using System.Text.Json.Serialization;

namespace BookPublishingAPI.Entities
{
    public class Publishing
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime PublishDate { get; set; }

        [JsonIgnore]
        public virtual Author Author { get; set; }
        [JsonIgnore]
        public virtual Book Book { get; set; }
    }
}
