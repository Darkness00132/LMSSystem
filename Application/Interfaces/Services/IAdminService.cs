using Application.Dtos.Users;

namespace Application.Interfaces.Services
{
    public interface IAdminService
    {
        Task<IEnumerable<UserDto>> GetUsersAsync();

        Task PromoteToInstructorAsync(Guid userId);

        Task PromoteToAssistantAsync(Guid userId);

        Task DeleteUserAsync(Guid userId);
    }
}
