namespace Application.Dtos.Quizzes
{
    public class QuizAnswerDto
    {
        public Guid? SelectedOptionId { get; set; }

        public string? TextAnswer { get; set; }

        public bool IsCorrect { get; set; }
    }
}
