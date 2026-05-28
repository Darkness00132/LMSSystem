using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;

namespace Domain.Entities
{
    [Index(nameof(UserId), nameof(CourseId), IsUnique = true)]
    public class Enrollment
    {
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid CourseId { get; set; }

        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

        public double ProgressPercentage { get; set; }

        public EnrollmentStatus Status { get; set; }

        public User User { get; set; }
        public Course Course { get; set; }
    }
}