using Application.Dtos.BookDtos;

namespace Application.Dtos.AuthorDtos;

public class AuthorReadDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public ICollection<BookReadDtoForAuthor> Books { get; set; } = new List<BookReadDtoForAuthor>();
}

