using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Sections
{
    public class UpdateSectionRequest
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public int OrderIndex { get; set; }
    }
}
