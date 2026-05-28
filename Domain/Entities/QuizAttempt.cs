using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
    [Index(nameof(UserId), nameof(QuizId))]
    public class QuizAttempt
    {
        public Guid Id { get; set; }

        [Required]
        public Guid QuizId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public double Score { get; set; }

        public DateTime StartedAt { get; set; }
        public DateTime SubmittedAt { get; set; }

        public Quiz Quiz { get; set; }
        public User User { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}