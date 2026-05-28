using Application.Dtos.Users;
using Application.Interfaces.Services;

namespace Application.Services
{
    public class AdminService : IAdminService
    {
        public Task DeleteUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task PromoteToAssistantAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task PromoteToInstructorAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
