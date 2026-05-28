using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance
{
    public class AppDbContext : IdentityDbContext<User, Role, Guid>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<ContentItem> ContentItems { get; set; }

        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Assignment> Assignments { get; set; }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }

        public DbSet<QuizAttempt> QuizAttempts { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public DbSet<Submission> Submissions { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<ContentProgress> ContentProgresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ── TPT one-to-one owned content ──────────────────────────────
            modelBuilder.Entity<ContentItem>()
                .HasOne<Lesson>()
                .WithOne(l => l.ContentItem)
                .HasForeignKey<Lesson>(l => l.ContentItemId);

            modelBuilder.Entity<ContentItem>()
                .HasOne<Quiz>()
                .WithOne(q => q.ContentItem)
                .HasForeignKey<Quiz>(q => q.ContentItemId);

            modelBuilder.Entity<ContentItem>()
                .HasOne<Assignment>()
                .WithOne(a => a.ContentItem)
                .HasForeignKey<Assignment>(a => a.ContentItemId);

            // ── Course → Instructor ───────────────────────────────────────
            // Restrict: removing an instructor must not silently wipe courses.
            // Handle in app layer (reassign / soft-delete instructor first).
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Instructor)
                .WithMany(u => u.Courses)
                .HasForeignKey(c => c.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Price precision (suppresses the warning too)
            modelBuilder.Entity<Course>()
                .Property(c => c.Price)
                .HasPrecision(18, 2);

            // ── Enrollment → Course ───────────────────────────────────────
            // Restrict: don't silently delete enrollments when a course is deleted.
            // Force explicit cleanup so students get notified / refunded first.
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Answer → Question ─────────────────────────────────────────
            // Restrict: Answer is owned by QuizAttempt (cascade comes from there).
            // QuestionId is a reference only — deleting a question must not
            // try to cascade-delete answers through a second path.
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany()
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}