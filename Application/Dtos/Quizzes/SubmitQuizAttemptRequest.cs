namespace Application.Dtos.Quizzes
{
    public class SubmitQuizAttemptRequest
    {
        public List<QuizAnswerDto> Answers { get; set; } = [];
    }
}
