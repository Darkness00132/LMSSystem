using Application.Dtos.Enrollments;
using Application.Interfaces.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSSystem.Controllers
{
    /// <summary>
    /// Manages course enrollments between students and courses.
    /// </summary>
    [Route("api")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        /// <summary>
        /// Retrieves the currently logged-in student's enrollments.
        /// </summary>
        [Authorize(Roles = nameof(UserRole.Student))]
        [HttpGet("enrollments/my")]
        public async Task<ActionResult<IEnumerable<EnrollmentDto>>> GetMyEnrollments()
        {
            var studentId = Guid.Parse(User.FindFirst("sub")!.Value);

            var result = await _enrollmentService
                .GetMyEnrollmentsAsync(studentId);

            return Ok(result);
        }

        /// <summary>
        /// Enrolls the current student into a course.
        /// </summary>
        [Authorize(Roles = nameof(UserRole.Student))]
        [HttpPost("enrollments/{courseId}")]
        public async Task<ActionResult<Guid>> EnrollCourse(Guid courseId)
        {
            var studentId = Guid.Parse(User.FindFirst("sub")!.Value);

            var enrollmentId = await _enrollmentService
                .EnrollCourseAsync(studentId, courseId);

            return Ok(enrollmentId);
        }

        /// <summary>
        /// Retrieves all enrollments for a specific course (Instructor view).
        /// </summary>
        [Authorize(Roles = nameof(UserRole.Instructor))]
        [HttpGet("courses/{courseId}/enrollments")]
        public async Task<ActionResult<IEnumerable<EnrollmentDto>>> GetCourseEnrollments(Guid courseId)
        {
            var result = await _enrollmentService
                .GetCourseEnrollmentsAsync(courseId);

            return Ok(result);
        }

        /// <summary>
        /// Approves a student enrollment request.
        /// </summary>
        [Authorize(Roles = nameof(UserRole.Instructor))]
        [HttpPost("enrollments/{enrollmentId}/approval")]
        public async Task<ActionResult> ApproveEnrollment(Guid enrollmentId)
        {
            await _enrollmentService
                .ApproveEnrollmentAsync(enrollmentId);

            return NoContent();
        }
    }
}