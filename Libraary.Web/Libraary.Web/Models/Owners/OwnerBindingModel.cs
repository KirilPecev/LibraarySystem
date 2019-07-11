namespace Libraary.Web.Models.Owners
{
    using System.ComponentModel.DataAnnotations;

    public class OwnerBindingModel
    {
        [Required]
        public string LibraryId { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
