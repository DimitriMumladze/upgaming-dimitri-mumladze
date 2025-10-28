using Application.Dtos.BookDtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookReadDto>()
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));

        CreateMap<BookCreateDto, Book>();
        CreateMap<BookUpdateDto, Book>();
        CreateMap<Book, BookReadDtoForAuthor>();
    }
}
