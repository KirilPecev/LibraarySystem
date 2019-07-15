namespace Libraary.Services.DTOs.Publisher
{
    using Address;

    public class AddPublisherDTO
    {
        public string Name { get; set; }

        public AddressDTO Address { get; set; }

        public string URLAddress { get; set; }

        public string PhoneNumber { get; set; }
    }
}
