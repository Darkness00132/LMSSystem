    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.EntityFrameworkCore;
    using Domain.Enums;

    namespace Domain.Entities
    {
        [Index(nameof(InstructorId))]
        public class Course
        {
            public Guid Id { get; set; }

            [Required, MaxLength(250)]
            public string Title { get; set; }

            [MaxLength(2000)]
            public string Description { get; set; }

            public string ThumbnailUrl { get; set; }

            [Required]
            public Guid InstructorId { get; set; }

            public decimal? Price { get; set; }

            public CourseStatus Status { get; set; }

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

            [ForeignKey(nameof(InstructorId))]
            public User Instructor { get; set; }

            public ICollection<Section> Sections { get; set; }
            public ICollection<Enrollment> Enrollments { get; set; }
        }
    }