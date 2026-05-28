using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    [Index(nameof(AttemptId), nameof(QuestionId), IsUnique = true)]
    public class Answer
    {
        public Guid Id { get; set; }

        [Required]
        public Guid AttemptId { get; set; }

        [Required]
        public Guid QuestionId { get; set; }

        public Guid? SelectedOptionId { get; set; }

        public string TextAnswer { get; set; }

        public bool IsCorrect { get; set; }

        public QuizAttempt Attempt { get; set; }
        public Question Question { get; set; }
        public Option SelectedOption { get; set; }
    }
}