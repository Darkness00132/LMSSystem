using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Auth
{
    public class RegisterRequest
    {
        [StringLength(100, MinimumLength = 3)]
        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
