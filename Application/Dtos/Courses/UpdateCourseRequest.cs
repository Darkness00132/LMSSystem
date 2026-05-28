using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Courses
{
    public class UpdateCourseRequest
    {
        [Required]
        [MaxLength(250)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string? Description { get; set; }

        public string? ThumbnailUrl { get; set; }

        public decimal? Price { get; set; }
    }
}