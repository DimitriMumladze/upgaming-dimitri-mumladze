namespace Application.Dtos.BookDtos;

public class BookUpdateDto
{
    public string Title { get; set; } = default!;
    public int AuthorId { get; set; }
    public int PublicationYear { get; set; }
}
