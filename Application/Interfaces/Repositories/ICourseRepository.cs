using Application.Dtos.Courses;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface ICourseRepository
    {
        Task CreateAsync(Course course);

        Task<Course?> GetByIdAsync(Guid id);

        Task<CourseDetailsDto?> GetPublishedCourseAsync(Guid courseId);

        Task<IEnumerable<CourseDto>> GetPublishedCoursesAsync();
    }
}
