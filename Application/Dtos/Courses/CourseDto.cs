namespace Application.Dtos.Courses
{
    public class CourseDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? ThumbnailUrl { get; set; }

        public decimal? Price { get; set; }
    }
}
