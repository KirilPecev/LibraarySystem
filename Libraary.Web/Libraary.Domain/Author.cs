namespace Libraary.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Author : BaseModel<string>
    {
        public Author()
        {
            this.AuthorBooks = new HashSet<AuthorBooks>();
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

        public virtual ICollection<AuthorBooks> AuthorBooks { get; set; }

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName}";
        }
    }
}
