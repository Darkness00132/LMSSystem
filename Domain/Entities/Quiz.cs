using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Quiz
    {
        [Key]
        public Guid ContentItemId { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int? TimeLimit { get; set; }

        public int PassingScore { get; set; }

        public ContentItem ContentItem { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}