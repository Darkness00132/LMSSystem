using Application.Dtos.Courses;
using Application.Interfaces.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSSystem.Controllers
{
    /// <summary>
    /// Manages course operations including creation, publishing, and review.
    /// </summary>
    [Route("api/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        /// <summary>
        /// Retrieves all published courses.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetPublishedCourses()
        {
            var courses = await _courseService.GetPublishedCoursesAsync();

            return Ok(courses);
        }

        /// <summary>
        /// Retrieves details of a published course.
        /// </summary>
        [HttpGet("{courseId}")]
        public async Task<ActionResult<CourseDetailsDto>> GetPublishedCourse(
            Guid courseId)
        {
            var course = await _courseService
                .GetPublishedCourseAsync(courseId);

            return Ok(course);
        }

        /// <summary>
        /// Creates a new course.
        /// </summary>
        [Authorize(Roles = nameof(UserRole.Instructor))]
        [HttpPost]
        public async Task<ActionResult> CreateCourse(
            CreateCourseRequest request)
        {
            var courseId = await _courseService
                .CreateCourseAsync(request);

            return CreatedAtAction(
                nameof(GetPublishedCourse),
                new { courseId },
                null);
        }

        /// <summary>
        /// Updates an existing course.
        /// </summary>
        [Authorize(Roles = nameof(UserRole.Instructor))]
        [HttpPut("{courseId}")]
        public async Task<ActionResult> UpdateCourse(
            Guid courseId,
            UpdateCourseRequest request)
        {
            await _courseService
                .UpdateCourseAsync(courseId, request);

            return NoContent();
        }

        /// <summary>
        /// Submits a course for admin review.
        /// </summary>
        [Authorize(Roles = nameof(UserRole.Instructor))]
        [HttpPost("{courseId}/submit")]
        public async Task<ActionResult> SubmitCourseToAdmin(Guid courseId)
        {
            await _courseService
                .SubmitCourseToAdminAsync(courseId);

            return NoContent();
        }

        /// <summary>
        /// Removes a course from public publishing.
        /// </summary>
        [Authorize(Roles = nameof(UserRole.Instructor))]
        [HttpPost("{courseId}/unpublish")]
        public async Task<ActionResult> UnpublishCourse(Guid courseId)
        {
            await _courseService
                .UnpublishCourseAsync(courseId);

            return NoContent();
        }

        /// <summary>
        /// Reviews and approves or rejects a submitted course.
        /// </summary>
        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPut("{courseId}/review")]
        public async Task<ActionResult> ReviewCourse(
            Guid courseId,
            ReviewCourseRequest request)
        {
            await _courseService
                .ReviewCourseAsync(courseId, request);

            return NoContent();
        }
    }
}