namespace Application.Dtos.BookDtos;

public class BookCreateDto
{
    public string Title { get; set; } = default!;
    public int AuthorId { get; set; }
    public int PublicationYear { get; set; }
}
