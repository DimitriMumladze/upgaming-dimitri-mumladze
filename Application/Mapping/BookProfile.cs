using Application.Dtos.BookDtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookReadDto>();
    }
}
