namespace Libraary.Domain
{
    using System.ComponentModel.DataAnnotations;

    public class Address : BaseModel<string>
    {
        [Required]
        [MaxLength(60)]
        public string Country { get; set; }

        [Required]
        [MaxLength(60)]
        public string Town { get; set; }

        [Required]
        [MaxLength(80)]
        public string Street { get; set; }
    }
}
