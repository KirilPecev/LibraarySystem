namespace Libraary.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Common.DataConstants.Library;

    public class Library : BaseModel<string>
    {
        public Library()
        {
            this.LibraryBooks = new HashSet<LibraryBook>();
            this.LibraryUsers = new HashSet<LibraaryUser>();
            this.Authors = new HashSet<Author>();
        }

        [Required]
        [MaxLength(LibraryNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string AddressId { get; set; }
        public virtual Address Address { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public virtual ICollection<LibraryBook> LibraryBooks { get; set; }

        public virtual ICollection<LibraaryUser> LibraryUsers { get; set; }

        public virtual ICollection<Author> Authors { get; set; }
    }
}
