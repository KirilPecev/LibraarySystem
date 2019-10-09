namespace Libraary.Web.Models.Librarians
{
    using System.ComponentModel.DataAnnotations;
    using static Common.DataConstants.Librarian;

    public class LibrarianBindingModel
    {
        [Required]
        public string LibraryId { get; set; }

        public string Library { get; set; }

        [Required]
        [Display(Name = DisplayEmail)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
