using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
    [Index(nameof(UserId), nameof(AssignmentId))]
    public class Submission
    {
        public Guid Id { get; set; }

        [Required]
        public Guid AssignmentId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public string? FileUrl { get; set; }
        public string? TextSubmission { get; set; }

        public double? Score { get; set; }

        public string Feedback { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
        public DateTime? GradedAt { get; set; }

        public Assignment Assignment { get; set; }
        public User User { get; set; }
    }
}