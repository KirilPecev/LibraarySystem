namespace Libraary.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Author : BaseModel<string>
    {
        public Author()
        {
            this.Books = new HashSet<Book>();
        }

        [Required]
        [MaxLength(25)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(25)]
        public string LastName { get; set; }

        [Required]
        public string AddressId { get; set; }
        public virtual Address Address { get; set; }

        public string LibraryId { get; set; }
        public Library Library { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName}";
        }
    }
}
