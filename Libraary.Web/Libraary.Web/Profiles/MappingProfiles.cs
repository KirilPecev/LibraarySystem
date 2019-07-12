namespace Libraary.Web.Profiles
{
    using AutoMapper;
    using Libraary.Services.DTOs.Book;
    using Libraary.Services.DTOs.Librarian;
    using Libraary.Services.DTOs.Owner;
    using Libraary.Web.Models.Books;
    using Libraary.Web.Models.Librarians;
    using Models.Address;
    using Models.Libraries;
    using Models.Owners;
    using Services.DTOs.Address;
    using Services.DTOs.Library;

    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AddressInputModel, AddressDTO>();
            CreateMap<AddLibraryInputModel, AddLibraryDTO>();
            CreateMap<OwnerBindingModel, OwnerDTO>();
            CreateMap<LibraryDTO, LibraryViewModel>();
            CreateMap<LibraryDetailsDTO, LibraryDetailsViewModel>();
            CreateMap<LibraryDetailsDTO, LibraryDetailsViewModel>();
            CreateMap<LibrarianBindingModel, LibrarianDTO>();
            CreateMap<LibrarianDetailsDTO, LibrarianDetailsViewModel>();
            CreateMap<BookDTO, BookViewModel>();
        }
    }
}
