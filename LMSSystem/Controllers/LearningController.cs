using Application.Dtos.CourseItems;
using Application.Dtos.Courses;
using Application.Interfaces.Services;
using Domain.Enums;
using Application.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSSystem.Controllers
{
    /// <summary>
    /// Manages student learning operations including course access and progress tracking.
    /// </summary>
    [Route("api")]
    [ApiController]
    public class LearningController : ControllerBase
    {
        private readonly ILearningService _learningService;

        public LearningController(ILearningService learningService)
        {
            _learningService = learningService;
        }

        /// <summary>
        /// Retrieves course syllabus for an enrolled student.
        /// Includes sections and content titles only.
        /// </summary>
        [Authorize(Roles = nameof(UserRole.Student))]
        [HttpGet("courses/{courseId}/learn")]
        public async Task<ActionResult<CourseDetailsDto>> LearnCourse(
            Guid courseId)
        {
            var studentId = Guid.Parse(User.FindFirst(AppClaims.UserId)!.Value);

            var result = await _learningService
                .LearnCourseAsync(studentId, courseId);

            return Ok(result);
        }

        /// <summary>
        /// Retrieves detailed content for an enrolled student.
        /// Content can be lesson, quiz, assignment, or other learning material.
        /// </summary>
        [Authorize(Roles = nameof(UserRole.Student))]
        [HttpGet("content/{contentId}")]
        public async Task<ActionResult<LearningContent>> GetContent(
            Guid contentId)
        {
            var studentId = Guid.Parse(User.FindFirst(AppClaims.UserId)!.Value);

            var result = await _learningService
                .GetContentAsync(studentId, contentId);

            return Ok(result);
        }

        /// <summary>
        /// Marks a content item as completed for the current student.
        /// </summary>
        [Authorize(Roles = nameof(UserRole.Student))]
        [HttpPost("progress/{contentItemId}/complete")]
        public async Task<ActionResult> CompleteContent(
            Guid contentItemId)
        {
            var studentId = Guid.Parse(User.FindFirst(AppClaims.UserId)!.Value);

            await _learningService
                .CompleteContentAsync(studentId, contentItemId);

            return NoContent();
        }
    }
}
