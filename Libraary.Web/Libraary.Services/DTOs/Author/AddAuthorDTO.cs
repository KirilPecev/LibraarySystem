namespace Libraary.Services.DTOs.Author
{
    using Address;

    public class AddAuthorDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public AddressDTO Address { get; set; }
    }
}
