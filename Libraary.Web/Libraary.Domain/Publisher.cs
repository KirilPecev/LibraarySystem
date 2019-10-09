namespace Libraary.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Common.DataConstants.Publisher;

    public class Publisher : BaseModel<string>
    {
        public Publisher()
        {
            this.Books = new HashSet<Book>();
        }

        [Required]
        [MaxLength(PublisherNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [Url]
        public string URLAddress { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
