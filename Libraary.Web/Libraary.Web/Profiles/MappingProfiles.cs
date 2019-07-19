﻿namespace Libraary.Web.Profiles
{
    using AutoMapper;
    using Libraary.Services.DTOs.Author;
    using Libraary.Services.DTOs.Book;
    using Libraary.Services.DTOs.Librarian;
    using Libraary.Services.DTOs.Owner;
    using Libraary.Services.DTOs.Publisher;
    using Libraary.Web.Models.Authors;
    using Libraary.Web.Models.Books;
    using Libraary.Web.Models.Librarians;
    using Libraary.Web.Models.Publishers;
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