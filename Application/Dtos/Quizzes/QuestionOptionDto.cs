namespace Application.Dtos.Quizzes
{
    public class QuestionOptionDto
    {
        public string OptionText { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }
    }
}
