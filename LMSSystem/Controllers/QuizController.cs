using Application.Dtos.Quizzes;
using Application.Interfaces.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSSystem.Controllers
{
    /// <summary>
    /// Manages quiz questions, attempts, and quiz results.
    /// </summary>
    [Route("api")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        /// <summary>
        /// Adds a new question to a quiz.
        /// </summary>
        [Authorize(Roles = $"{nameof(UserRole.Instructor)},{nameof(UserRole.Assistant)}")]
        [HttpPost("quizzes/{quizId}/questions")]
        public async Task<ActionResult<Guid>> AddQuestionToQuiz(
            Guid quizId,
            CreateQuestionRequest request)
        {
            var questionId = await _quizService
                .AddQuestionToQuizAsync(quizId, request);

            return Ok(questionId);
        }

        /// <summary>
        /// Updates an existing quiz question.
        /// </summary>
        [Authorize(Roles = $"{nameof(UserRole.Instructor)},{nameof(UserRole.Assistant)}")]
        [HttpPut("questions/{questionId}")]
        public async Task<ActionResult> UpdateQuestion(
            Guid questionId,
            UpdateQuestionRequest request)
        {
            await _quizService
                .UpdateQuestionAsync(questionId, request);

            return NoContent();
        }

        /// <summary>
        /// Deletes a question from a quiz.
        /// </summary>
        [Authorize(Roles = $"{nameof(UserRole.Instructor)},{nameof(UserRole.Assistant)}")]
        [HttpDelete("questions/{questionId}")]
        public async Task<ActionResult> DeleteQuestion(Guid questionId)
        {
            await _quizService
                .DeleteQuestionAsync(questionId);

            return NoContent();
        }

        /// <summary>
        /// Starts a new quiz attempt for the current student.
        /// </summary>
        [Authorize(Roles = nameof(UserRole.Student))]
        [HttpPost("quizzes/{quizId}/start")]
        public async Task<ActionResult<Guid>> StartQuiz(Guid quizId)
        {
            var studentId = Guid.Parse(User.FindFirst("sub")!.Value);

            var attemptId = await _quizService
                .StartQuizAsync(studentId, quizId);

            return Ok(attemptId);
        }

        /// <summary>
        /// Submits answers for a quiz attempt.
        /// </summary>
        [Authorize(Roles = nameof(UserRole.Student))]
        [HttpPost("quiz-attempts/{attemptId}/submit")]
        public async Task<ActionResult> SubmitQuizAttempt(
            Guid attemptId,
            SubmitQuizAttemptRequest request)
        {
            await _quizService
                .SubmitQuizAttemptAsync(attemptId, request);

            return NoContent();
        }

        /// <summary>
        /// Retrieves the result of a quiz attempt.
        /// </summary>
        [Authorize(Roles = nameof(UserRole.Student))]
        [HttpGet("quiz-attempts/{attemptId}/result")]
        public async Task<ActionResult<QuizAttemptResultDto>> GetQuizAttemptResult(
            Guid attemptId)
        {
            var result = await _quizService
                .GetQuizAttemptResultAsync(attemptId);

            return Ok(result);
        }
    }
}