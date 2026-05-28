using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
    [Index(nameof(QuestionId))]
    public class Option
    {
        public Guid Id { get; set; }

        [Required]
        public Guid QuestionId { get; set; }

        [Required]
        public string OptionText { get; set; }

        public bool IsCorrect { get; set; }

        public Question Question { get; set; }
    }
}