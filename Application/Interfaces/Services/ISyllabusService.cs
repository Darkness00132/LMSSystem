using Application.Dtos.Sections;
using Application.Dtos.CourseItems;

namespace Application.Interfaces.Services
{
    public interface ISyllabusService
    {
        // Sections
        Task<Guid> CreateSectionAsync(Guid courseId, CreateSectionRequest request);

        Task UpdateSectionAsync(Guid sectionId, UpdateSectionRequest request);

        Task DeleteSectionAsync(Guid sectionId);

        Task ReorderSectionsAsync(Guid courseId, ReorderSectionsRequest request);

        // Content
        Task<Guid> AddContentToSectionAsync(Guid sectionId, CreateContentItemRequest request);

        Task UpdateContentAsync(Guid contentId, UpdateContentRequest request);

        Task DeleteContentAsync(Guid contentId);

        Task ReorderContentAsync(Guid sectionId, ReorderContentsRequest request);
    }
}