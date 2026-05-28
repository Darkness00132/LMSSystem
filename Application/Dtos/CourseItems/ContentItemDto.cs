using Domain.Enums;

namespace Application.Dtos.CourseItems
{
    public class ContentItemDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public ContentItemType Type { get; set; }

        public int OrderIndex { get; set; }

        public bool IsPreview { get; set; }
    }
}
