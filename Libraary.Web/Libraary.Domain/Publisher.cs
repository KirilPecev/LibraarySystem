namespace Libraary.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Publisher : BaseModel<string>
    {
        public Publisher()
        {
            this.Books = new HashSet<Book>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string AddressId { get; set; }
        public Address Address { get; set; }

        [Required]
        [Url]
        public string URLAddress { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
