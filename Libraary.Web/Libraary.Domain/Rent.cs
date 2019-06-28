namespace Libraary.Domain
{
    using System;
    using System.Collections.Generic;

    public class Rent
    {
        public Rent()
        {
            this.Books = new HashSet<Book>();
        }

        public string UserId { get; set; }
        public LibraaryUser User { get; set; }

        public DateTime IssuedOn { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
