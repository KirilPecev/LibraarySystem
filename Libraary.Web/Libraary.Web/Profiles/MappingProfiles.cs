namespace Libraary.Web.Profiles
{
    using AutoMapper;
    using Models.Address;
    using Models.Authors;
    using Models.Books;
    using Models.Librarians;
    using Models.Libraries;
    using Models.Publishers;
    using Services.DTOs.Address;
    using Services.DTOs.Author;
    using Services.DTOs.Book;
    using Services.DTOs.Librarian;
    using Services.DTOs.Library;
    using Services.DTOs.Publisher;

    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AddressInputModel, AddressDTO>();
            CreateMap<AddLibraryInputModel, AddLibraryDTO>();
            CreateMap<LibraryDTO, LibraryViewModel>();
            CreateMap<LibraryDetailsDTO, LibraryDetailsViewModel>();
            CreateMap<LibraryDetailsDTO, LibraryDetailsViewModel>();
            CreateMap<LibrarianBindingModel, LibrarianDTO>();
            CreateMap<LibrarianDetailsDTO, LibrarianDetailsViewModel>();
            CreateMap<BookDTO, BookViewModel>();
            CreateMap<BookInputModel, AddBookDTO>();
            CreateMap<BookDetailsDTO, BookDetailsViewModel>();
            CreateMap<AuthorBindingModel, AddAuthorDTO>();
            CreateMap<AuthorViewDTO, AuthorViewModel>();
            CreateMap<PublisherBindingModel, AddPublisherDTO>();
            CreateMap<PublisherViewModelDTO, PublisherViewModel>();
            CreateMap<EditBookDto, BookEditInputModel>();
            CreateMap<BookEditInputModel, EditBookDto>();
        }
    }
}
