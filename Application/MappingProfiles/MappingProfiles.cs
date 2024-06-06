using Application.Models.Author;
using Application.Models.Book;
using AutoMapper;
using Domain;

namespace Application.MappingProfiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // Author Mappings
        CreateMap<Author, AuthorDto>().ReverseMap();
        CreateMap<Author, AuthorDetailsDto>().ReverseMap();
        CreateMap<CreateAuthorDto, Author>();
        CreateMap<UpdateAuthorDto, Author>().ReverseMap();

        // Book Mappings
        CreateMap<Book, BookListDto>()
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors));

        CreateMap<Book, BookDetailsDto>().ReverseMap();
        CreateMap<CreateBookDto, Book>();
        CreateMap<UpdateBookDto, Book>().ReverseMap();
    }
}
