using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;

namespace Domain.Entities
{
    [Index(nameof(QuizId))]
    public class Question
    {
        public Guid Id { get; set; }

        [Required]
        public Guid QuizId { get; set; }

        [Required]
        public string QuestionText { get; set; }

        public QuestionType QuestionType { get; set; }

        public int Points { get; set; }

        public Quiz Quiz { get; set; }

        public ICollection<Option> Options { get; set; }
    }
}