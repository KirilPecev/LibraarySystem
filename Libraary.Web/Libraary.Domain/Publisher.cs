namespace Libraary.Domain
{
    public class Publisher : BaseModel<string>
    {
        public string Name { get; set; }

        public string AddressId { get; set; }
        public Address Address { get; set; }

        public string URLAddress { get; set; }

        public string PhoneNumber { get; set; }
    }
}
