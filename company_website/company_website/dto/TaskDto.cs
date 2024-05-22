namespace company_website.dto
{
    public class TaskDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? TaskDescription { get; set; }

        public string ExpectedEndDate { get; set; }

        public string StartDate { get; set; }

        public int? TeamSize { get; set; }

        public string? Status { get; set; }

        public IFormFile? Thumbail { get; set; }
        public string? ThumbnailBase64 {  get; set; }

    }
}
