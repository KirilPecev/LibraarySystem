﻿namespace Libraary.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Library : BaseModel<string>
    {
        public Library()
        {
            this.LibraryBooks = new HashSet<LibraryBook>();
            this.LibraryUsers = new HashSet<LibraaryUser>();
            this.Authors = new HashSet<Author>();
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
        [Range(typeof(decimal), "0.01", "1000")]
        public decimal BooksFee { get; set; }

        public virtual ICollection<LibraryBook> LibraryBooks { get; set; }

        public virtual ICollection<LibraaryUser> LibraryUsers { get; set; }

        public virtual ICollection<Author> Authors { get; set; }
    }
}
