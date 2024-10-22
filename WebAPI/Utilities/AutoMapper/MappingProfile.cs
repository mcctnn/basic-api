using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Model;

namespace WebAPI.Utilities.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<BookDtoForUpdate, Book>().ReverseMap();
            CreateMap<Book, BookDto>();
            CreateMap<BookDtoForInsertion, Book>();
            CreateMap<UserForRegistrationDto, User>();
            CreateMap<CategoryDtoForUpdate, Category>().ReverseMap();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDtoForInsertion,Category>();
        }
    }
}
