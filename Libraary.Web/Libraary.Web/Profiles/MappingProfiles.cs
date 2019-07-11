namespace Libraary.Web.Profiles
{
    using AutoMapper;
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
        }
    }
}
