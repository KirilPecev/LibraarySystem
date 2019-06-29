namespace Libraary.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Rent
    {
        [Required]
        public string UserId { get; set; }
        public LibraaryUser User { get; set; }

        [Required]
        public string BookId { get; set; }
        public Book Book { get; set; }

        [Required]
        public DateTime IssuedOn { get; set; }
    }
}
