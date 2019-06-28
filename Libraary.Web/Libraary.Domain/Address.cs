namespace Libraary.Domain
{
    public class Address : BaseModel<string>
    {
        public string Country { get; set; }

        public string Town { get; set; }

        public string Street { get; set; }
    }
}
