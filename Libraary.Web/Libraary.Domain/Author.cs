namespace Libraary.Domain
{
    public class Author : BaseModel<string>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AddressId { get; set; }
        public Address Address { get; set; }
    }
}
