using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                    Name = UserRole.Student.ToString(),
                    NormalizedName = UserRole.Student.ToString().ToUpper(),
                    ConcurrencyStamp = "00000000-0000-0000-0000-000000000000"
                },

                new Role
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Name = UserRole.Instructor.ToString(),
                    NormalizedName = UserRole.Instructor.ToString().ToUpper(),
                    ConcurrencyStamp = "00000000-0000-0000-0000-000000000001"
                },
                new Role
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Name = UserRole.Assistant.ToString(),
                    NormalizedName = UserRole.Assistant.ToString().ToUpper(),
                    ConcurrencyStamp = "00000000-0000-0000-0000-000000000002"
                },
                new Role
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    Name = UserRole.Admin.ToString(),
                    NormalizedName = UserRole.Admin.ToString().ToUpper(),
                    ConcurrencyStamp = "00000000-0000-0000-0000-000000000003"
                }
            );
        }
    }
}