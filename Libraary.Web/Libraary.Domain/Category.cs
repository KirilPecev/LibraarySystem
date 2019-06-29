namespace Libraary.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category : BaseModel<string>
    {
        public Category()
        {
            this.BookCategories = new List<BookCategory>();
        }

        [Required]
        [MaxLength(70)]
        public string CategoryName { get; set; }

        public virtual ICollection<BookCategory> BookCategories { get; set; }
    }
}
