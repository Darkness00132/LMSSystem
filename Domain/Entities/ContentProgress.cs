using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
    [Index(nameof(UserId), nameof(ContentItemId), IsUnique = true)]
    public class ContentProgress
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public Guid ContentItemId { get; set; }

        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }

        public User User { get; set; }
        public ContentItem ContentItem { get; set; }
    }
}