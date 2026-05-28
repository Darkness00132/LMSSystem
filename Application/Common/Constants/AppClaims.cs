using System.Security.Claims;

namespace Application.Common.Constants
{
    public static class AppClaims
    {
        public const string UserId = ClaimTypes.NameIdentifier;
        public const string Email = ClaimTypes.Email;
        public const string Role = ClaimTypes.Role;
        public const string FullName = "full_name";
        public const string InstructorId = "instructor_id"; // only for Assistants
    }
}
