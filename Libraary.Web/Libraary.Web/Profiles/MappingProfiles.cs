namespace Libraary.Web.Profiles
{
    using AutoMapper;
    using Models.Address;
    using Models.Libraries;
    using Services.DTOs.Address;
    using Services.DTOs.Library;

    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AddressInputModel, AddressDTO>();
            CreateMap<AddLibraryInputModel, AddLibraryDTO>();
            CreateMap<OwnerBindingModel, OwnerDTO>();
        }
    }
}
