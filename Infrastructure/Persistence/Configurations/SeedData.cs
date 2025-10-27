using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations;

public static class SeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>().HasData(
            new Author
            {
                Id = 1,
                Name = "J.K. Rowling"
            },
            new Author
            {
                Id = 2,
                Name = "George Orwell"
            },
            new Author
            {
                Id = 3,
                Name = "J.R.R. Tolkien"
            }
        );

        modelBuilder.Entity<Book>().HasData(
            new Book
            {
                Id = 1,
                Title = "Harry Potter and the Philosopher's Stone",
                AuthorId = 1,
                PublicationYear = 1997
            },
            new Book
            {
                Id = 2,
                Title = "Harry Potter and the Chamber of Secrets",
                AuthorId = 1,
                PublicationYear = 1998
            },
            new Book
            {
                Id = 3,
                Title = "1984",
                AuthorId = 2,
                PublicationYear = 1949
            },
            new Book
            {
                Id = 4,
                Title = "Animal Farm",
                AuthorId = 2,
                PublicationYear = 1945
            },
            new Book
            {
                Id = 5,
                Title = "The Hobbit",
                AuthorId = 3,
                PublicationYear = 1937
            },
            new Book
            {
                Id = 6,
                Title = "The Lord of the Rings",
                AuthorId = 3,
                PublicationYear = 1954
            }
        );
    }
}