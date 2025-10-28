namespace Application.Dtos.BookDtos;

public class BookReadDtoForAuthor
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public int PublicationYear { get; set; }
}

