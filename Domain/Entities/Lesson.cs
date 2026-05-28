using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities
{
    public class Lesson
    {
        [Key]
        public Guid ContentItemId { get; set; }

        public LessonContentType ContentType { get; set; }

        public string? VideoUrl { get; set; }
        public string? TextContent { get; set; }
        public string? FileUrl { get; set; }

        public int? Duration { get; set; }

        public ContentItem ContentItem { get; set; }
    }
}