namespace Libraary.Services.DTOs.Library
{
    using Address;

    public class AddLibraryDTO
    {
        public string Name { get; set; }

        public AddressDTO Address { get; set; }

        public string PhoneNumber { get; set; }
    }
}
