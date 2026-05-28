using Domain.Enums;

namespace Application.Dtos.Enrollments
{
    public class EnrollmentDto
    {
        public Guid UserId { get; set; }

        public Guid CourseId { get; set; }

        public DateTime EnrolledAt { get; set; }

        public double ProgressPercentage { get; set; }

        public EnrollmentStatus Status { get; set; }
    }
}
