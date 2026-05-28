using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.CourseItems
{
    public class CreateContentItemRequest
    {
        [Required]
        public Guid SectionId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public ContentItemType Type { get; set; }

        public int OrderIndex { get; set; }

        public bool IsPreview { get; set; }
    }
}
