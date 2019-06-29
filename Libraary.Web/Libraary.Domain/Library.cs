namespace Libraary.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Library : BaseModel<string>
    {
        public Library()
        {
            this.LibraryBooks = new HashSet<LibraryBook>();
            this.LibraryUsers = new HashSet<LibraaryUser>();
        }

        [Required]
        [MaxLength(70)]
        public string Name { get; set; }

        [Required]
        public string AddressId { get; set; }
        public Address Address { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string OwnerId { get; set; }
        public LibraaryUser Owner { get; set; }

        public virtual ICollection<LibraryBook> LibraryBooks { get; set; }

        public virtual ICollection<LibraaryUser> LibraryUsers { get; set; }
    }
}
