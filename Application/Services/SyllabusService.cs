    using Application.Dtos.CourseItems;
using Application.Dtos.Sections;
using Application.Interfaces.Services;

namespace Application.Services
{
    public class SyllabusService : ISyllabusService
    {
        public Task<Guid> AddContentToSectionAsync(Guid sectionId, CreateContentItemRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateSectionAsync(Guid courseId, CreateSectionRequest request)
        {
            throw new NotImplementedException();
        }

        public Task DeleteContentAsync(Guid contentId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSectionAsync(Guid sectionId)
        {
            throw new NotImplementedException();
        }

        public Task ReorderContentAsync(Guid sectionId, ReorderContentsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task ReorderSectionsAsync(Guid courseId, ReorderSectionsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task UpdateContentAsync(Guid contentId, UpdateContentRequest request)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSectionAsync(Guid sectionId, UpdateSectionRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
