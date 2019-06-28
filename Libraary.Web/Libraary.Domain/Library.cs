namespace Libraary.Domain
{
    public class Library : BaseModel<string>
    {
        public string Name { get; set; }

        public string AddressId { get; set; }
        public Address Address { get; set; }

        public string PhoneNumber { get; set; }

        public string OwnerId { get; set; }
        public LibraaryUser Owner { get; set; }
    }
}
