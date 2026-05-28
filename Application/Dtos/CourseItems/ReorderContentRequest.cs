namespace Application.Dtos.CourseItems
{
    public class ReorderContentsRequest
    {
        public List<ContentOrderDto> Items { get; set; } = new();
    }

    public class ContentOrderDto
    {
        public Guid ContentId { get; set; }

        public int OrderIndex { get; set; }
    }
}