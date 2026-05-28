using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
    public class Assignment
    {
        [Key]
        public Guid ContentItemId { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int MaxScore { get; set; }

        public DateTime? DueDate { get; set; }

        public ContentItem ContentItem { get; set; }

        public ICollection<Submission> Submissions { get; set; }
    }
}