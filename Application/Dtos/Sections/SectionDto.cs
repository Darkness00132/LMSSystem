using Application.Dtos.CourseItems;

namespace Application.Dtos.Sections
{
    public class SectionDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public int OrderIndex { get; set; }

        public List<ContentItemDto> ContentItems { get; set; } = [];
    }
}