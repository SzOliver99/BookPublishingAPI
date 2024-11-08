namespace BookPublishingAPI.DTO
{
    public class PublishingDTO
    {
        public int AuthorId { get; set; }
        public int BookId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
