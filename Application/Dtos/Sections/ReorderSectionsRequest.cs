namespace Application.Dtos.Sections
{
    public class ReorderSectionsRequest
    {
        public List<SectionOrderDto> Sections { get; set; } = new();
    }

    public class SectionOrderDto
    {
        public Guid SectionId { get; set; }

        public int OrderIndex { get; set; }
    }
}
