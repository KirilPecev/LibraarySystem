namespace Libraary.Web.Models.Librarians
{
    using System.ComponentModel.DataAnnotations;

    public class LibrarianBindingModel
    {
        [Required]
        public string LibraryId { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
