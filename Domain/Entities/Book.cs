namespace Domain.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;

    public int AuthorId { get; set; }
    public Author Author { get; set; } = default!;

    public int PublicationYear { get; set; }
}
