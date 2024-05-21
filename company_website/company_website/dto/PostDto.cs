namespace company_website.dto
{
    public class PostDto
    {

        public int Id { get; set; }
        public int? CategoryId { get; set; }

        public string? Title { get; set; }

        public IFormFile? Thumbnail { get; set; }
        public string? ThumbnailBase64 { get; set; }

        public string? Content { get; set; }
        public string? Abstract { get; set; }
    }

}
