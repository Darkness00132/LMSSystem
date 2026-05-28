using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Sections
{
    public class CreateSectionRequest
    {
        [Required]
        public Guid CourseId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public int OrderIndex { get; set; }
    }
}