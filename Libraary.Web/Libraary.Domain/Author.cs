namespace Libraary.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Common.DataConstants.Author;

    public class Author : BaseModel<string>
    {
        public Author()
        {
            this.AuthorBooks = new HashSet<AuthorBooks>();
        }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(NationalityMaxLength)]
        public string Nationality { get; set; }

        public virtual ICollection<AuthorBooks> AuthorBooks { get; set; }

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName}";
        }
    }
}
