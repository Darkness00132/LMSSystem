using Application.Dtos.Enrollments;

namespace Application.Interfaces.Services
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<EnrollmentDto>> GetMyEnrollmentsAsync(Guid studentId);

        Task<Guid> EnrollCourseAsync(Guid studentId, Guid courseId);

        Task<IEnumerable<EnrollmentDto>> GetCourseEnrollmentsAsync(Guid courseId);

        Task ApproveEnrollmentAsync(Guid enrollmentId);
    }
}
