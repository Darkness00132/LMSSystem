using Application.Dtos.Courses;

namespace Application.Interfaces.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetPublishedCoursesAsync();

        Task<CourseDetailsDto> GetPublishedCourseAsync(Guid courseId);

        Task<Guid> CreateCourseAsync(Guid instructorId, CreateCourseRequest request);

        Task UpdateCourseAsync(
            Guid courseId,
            UpdateCourseRequest request);

        Task SubmitCourseToAdminAsync(Guid courseId);

        Task UnpublishCourseAsync(Guid courseId);

        Task ReviewCourseAsync(
            Guid courseId,
            ReviewCourseRequest request);
    }
}
