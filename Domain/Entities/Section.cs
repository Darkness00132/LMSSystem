using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
    [Index(nameof(CourseId), nameof(OrderIndex))]
    public class Section
    {
        public Guid Id { get; set; }

        [Required]
        public Guid CourseId { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        public int OrderIndex { get; set; }

        public Course Course { get; set; }
        public ICollection<ContentItem> ContentItems { get; set; }
    }
}