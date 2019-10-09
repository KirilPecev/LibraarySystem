namespace Libraary.Domain
{
    using System.ComponentModel.DataAnnotations;
    using static Common.DataConstants.Address;

    public class Address : BaseModel<string>
    {
        [Required]
        [MaxLength(CountryMaxLength)]
        public string Country { get; set; }

        [Required]
        [MaxLength(TownMaxLength)]
        public string Town { get; set; }

        [Required]
        [MaxLength(StreetMaxLength)]
        public string Street { get; set; }

        [Required]
        public string Zip { get; set; }

        public override string ToString()
        {
            return $"{this.Country}, {this.Town}, {this.Street} {this.Zip}";
        }
    }
}
