using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;

namespace Domain.Entities
{
    [Index(nameof(SectionId), nameof(OrderIndex))]
    public class ContentItem
    {
        public Guid Id { get; set; }

        [Required]
        public Guid SectionId { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        public ContentItemType Type { get; set; }

        public int OrderIndex { get; set; }

        public bool IsPreview { get; set; }

        public Section Section { get; set; }
    }
}