using Application.Dtos.Users;
using Application.Interfaces.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSSystem.Controllers
{
    /// <summary>
    /// Manages administrative operations for users.
    /// </summary>
    [Authorize(Roles = nameof(UserRole.Admin))]
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        /// <summary>
        /// Retrieves all system users.
        /// </summary>
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _adminService.GetUsersAsync();

            return Ok(users);
        }

        /// <summary>
        /// Promotes a user to instructor role.
        /// </summary>
        [HttpPut("users/{userId}/promote-instructor")]
        public async Task<ActionResult> PromoteUserToInstructor(Guid userId)
        {
            await _adminService.PromoteToInstructorAsync(userId);

            return NoContent();
        }

        /// <summary>
        /// Promotes a user to assistant role.
        /// </summary>
        [HttpPut("users/{userId}/promote-assistant")]
        public async Task<ActionResult> PromoteUserToAssistant(Guid userId)
        {
            await _adminService.PromoteToAssistantAsync(userId);

            return NoContent();
        }

        /// <summary>
        /// Deletes a user account from the system.
        /// </summary>
        [HttpDelete("users/{userId}")]
        public async Task<ActionResult> DeleteUser(Guid userId)
        {
            await _adminService.DeleteUserAsync(userId);

            return NoContent();
        }
    }
}