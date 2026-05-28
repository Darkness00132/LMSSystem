using Domain.Enums;

namespace Application.Dtos.CourseItems
{
    public class UpdateContentRequest
    {
        public string? Title { get; set; }
        public ContentItemType? Type { get; set; }

        public int? OrderIndex { get; set; }

        public bool? IsPreview { get; set; }
    }
}
