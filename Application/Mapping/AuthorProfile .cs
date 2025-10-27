using Application.Dtos.AuthorDtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<Author, AuthorReadDto>()
            .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));

        CreateMap<AuthorCreateDto, Author>();
        CreateMap<AuthorUpdateDto, Author>();
    }
}
