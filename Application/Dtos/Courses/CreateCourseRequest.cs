using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.Dtos.Courses
{
    public class CreateCourseRequest
    {
        [Required]
        [MaxLength(250)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string? Description { get; set; }

        public IFormFile? Thumbnail { get; set; }

        public decimal? Price { get; set; }
    }
}
