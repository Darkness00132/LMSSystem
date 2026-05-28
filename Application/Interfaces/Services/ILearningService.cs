using Application.Dtos.CourseItems;
using Application.Dtos.Courses;

namespace Application.Interfaces.Services
{
    public interface ILearningService
    {
        Task<CourseDetailsDto> LearnCourseAsync(
            Guid studentId,
            Guid courseId);

        Task<LearningContent> GetContentAsync(
            Guid studentId,
            Guid contentId);

        Task CompleteContentAsync(
            Guid studentId,
            Guid contentItemId);
    }
}