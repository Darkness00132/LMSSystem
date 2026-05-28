using Application.Dtos.Sections;
using Domain.Enums;

namespace Application.Dtos.Courses
{
    public class CourseDetailsDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? ThumbnailUrl { get; set; }

        public decimal? Price { get; set; }

        public CourseStatus Status { get; set; }

        public Guid InstructorId { get; set; }

        public List<SectionDto> Sections { get; set; } = [];
    }
}