namespace Domain.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int AuthorID { get; set; } 
    public int PublicationYear { get; set; }
}
