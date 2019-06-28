namespace Libraary.Domain
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class LibraaryUser : IdentityUser
    {
        public LibraaryUser()
        {
            this.RentedBooks = new List<Rent>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string LibraryId { get; set; }
        public Library Library { get; set; }

        public string AddressId { get; set; }
        public Address Address { get; set; }

        ICollection<Rent> RentedBooks { get; set; }
    }
}