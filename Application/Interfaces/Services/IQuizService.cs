using Application.Dtos.Quizzes;

namespace Application.Interfaces.Services
{
    public interface IQuizService
    {
        Task<Guid> AddQuestionToQuizAsync(
            Guid quizId,
            CreateQuestionRequest request);

        Task UpdateQuestionAsync(
            Guid questionId,
            UpdateQuestionRequest request);

        Task DeleteQuestionAsync(Guid questionId);

        Task<Guid> StartQuizAsync(
            Guid studentId,
            Guid quizId);

        Task SubmitQuizAttemptAsync(
            Guid attemptId,
            SubmitQuizAttemptRequest request);

        Task<QuizAttemptResultDto> GetQuizAttemptResultAsync(
            Guid attemptId);
    }
}