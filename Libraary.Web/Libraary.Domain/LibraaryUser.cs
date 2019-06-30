﻿namespace Libraary.Domain
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class LibraaryUser : IdentityUser<string>
    {
        public LibraaryUser()
        {
            this.RentedBooks = new List<Rent>();
        }

        [Required]
        [MaxLength(25)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(25)]
        public string LastName { get; set; }

        public string LibraryId { get; set; }
        public Library Library { get; set; }

        public string AddressId { get; set; }
        public Address Address { get; set; }

        public virtual ICollection<Rent> RentedBooks { get; set; }
    }
}