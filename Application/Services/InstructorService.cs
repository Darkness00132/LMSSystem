using Application.Dtos.Users;
using Application.Interfaces.Services;

namespace Application.Services
{
    public class InstructorService : IInstructorService
    {
        public Task AssignAssistantAsync(AssignAssistantRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDto>> GetAssistantsAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveAssistantAsync(Guid assistantId)
        {
            throw new NotImplementedException();
        }
    }
}
