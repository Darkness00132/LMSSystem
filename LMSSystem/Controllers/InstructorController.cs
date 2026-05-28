using Application.Dtos.Users;
using Application.Interfaces.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSSystem.Controllers
{
    /// <summary>
    /// Manages instructor operations related to assistants.
    /// </summary>
    [Authorize(Roles = nameof(UserRole.Instructor))]
    [Route("api/instructor")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorService _instructorService;

        public InstructorController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        /// <summary>
        /// Retrieves all assistants assigned to the instructor.
        /// </summary>
        [HttpGet("assistants")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAssistants()
        {
            var assistants = await _instructorService.GetAssistantsAsync();

            return Ok(assistants);
        }

        /// <summary>
        /// Assigns a user as an assistant to the instructor.
        /// </summary>
        [HttpPost("assistants")]
        public async Task<ActionResult> MakeAssistant(
            AssignAssistantRequest request)
        {
            await _instructorService.AssignAssistantAsync(request);

            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Removes an assistant from the instructor.
        /// </summary>
        [HttpDelete("assistants/{assistantId}")]
        public async Task<ActionResult> RemoveAssistant(Guid assistantId)
        {
            await _instructorService.RemoveAssistantAsync(assistantId);

            return NoContent();
        }
    }
}