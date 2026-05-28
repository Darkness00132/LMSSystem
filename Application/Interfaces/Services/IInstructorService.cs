using Application.Dtos.Users;

namespace Application.Interfaces.Services
{
    public interface IInstructorService
    {
        Task<IEnumerable<UserDto>> GetAssistantsAsync();

        Task AssignAssistantAsync(AssignAssistantRequest request);

        Task RemoveAssistantAsync(Guid assistantId);
    }
}