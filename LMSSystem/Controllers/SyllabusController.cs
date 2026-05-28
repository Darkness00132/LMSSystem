using Application.Dtos.Sections;
using Application.Dtos.CourseItems;
using Application.Interfaces.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSSystem.Controllers
{
    /// <summary>
    /// Manages course syllabus including sections and content items.
    /// Accessible by instructors and assistants.
    /// </summary>
    [Authorize(Roles = $"{nameof(UserRole.Instructor)},{nameof(UserRole.Assistant)}")]
    [Route("api")]
    [ApiController]
    public class SyllabusController : ControllerBase
    {
        private readonly ISyllabusService _syllabusService;

        public SyllabusController(ISyllabusService syllabusService)
        {
            _syllabusService = syllabusService;
        }

        /// <summary>
        /// Creates a new section inside a course.
        /// </summary>
        [HttpPost("courses/{courseId}/sections")]
        public async Task<ActionResult<Guid>> CreateSection(
            Guid courseId,
            [FromBody] CreateSectionRequest request)
        {
            var sectionId = await _syllabusService
                .CreateSectionAsync(courseId, request);

            return Ok(sectionId);
        }

        /// <summary>
        /// Updates an existing section.
        /// </summary>
        [HttpPut("sections/{sectionId}")]
        public async Task<IActionResult> UpdateSection(
            Guid sectionId,
            [FromBody] UpdateSectionRequest request)
        {
            await _syllabusService
                .UpdateSectionAsync(sectionId, request);

            return NoContent();
        }

        /// <summary>
        /// Deletes a section from a course.
        /// </summary>
        [HttpDelete("sections/{sectionId}")]
        public async Task<IActionResult> DeleteSection(Guid sectionId)
        {
            await _syllabusService
                .DeleteSectionAsync(sectionId);

            return NoContent();
        }

        /// <summary>
        /// Reorders sections inside a course.
        /// </summary>
        [HttpPut("courses/{courseId}/sections/reorder")]
        public async Task<IActionResult> ReOrderSections(
            Guid courseId,
            [FromBody] ReorderSectionsRequest request)
        {
            await _syllabusService
                .ReorderSectionsAsync(courseId, request);

            return NoContent();
        }

        /// <summary>
        /// Adds a content item (lesson, quiz, assignment, etc.) to a section.
        /// </summary>
        [HttpPost("sections/{sectionId}/content")]
        public async Task<ActionResult<Guid>> AddContentToSection(
            Guid sectionId,
            [FromBody] CreateContentItemRequest request)
        {
            var contentId = await _syllabusService
                .AddContentToSectionAsync(sectionId, request);

            return Ok(contentId);
        }

        /// <summary>
        /// Updates a content item inside a section.
        /// </summary>
        [HttpPut("content/{contentId}")]
        public async Task<IActionResult> UpdateContent(
            Guid contentId,
            [FromBody] UpdateContentRequest request)
        {
            await _syllabusService
                .UpdateContentAsync(contentId, request);

            return NoContent();
        }

        /// <summary>
        /// Deletes a content item.
        /// </summary>
        [HttpDelete("content/{contentId}")]
        public async Task<IActionResult> DeleteContent(Guid contentId)
        {
            await _syllabusService
                .DeleteContentAsync(contentId);

            return NoContent();
        }

        /// <summary>
        /// Reorders content items inside a section.
        /// </summary>
        [HttpPut("sections/{sectionId}/content/reorder")]
        public async Task<IActionResult> ReOrderContentsInSection(
            Guid sectionId,
            [FromBody] ReorderContentsRequest request)
        {
            await _syllabusService
                .ReorderContentAsync(sectionId, request);

            return NoContent();
        }
    }
}