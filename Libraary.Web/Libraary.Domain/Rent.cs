namespace Libraary.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Rent : BaseModel<string>
    {
        [Required]
        public string UserId { get; set; }
        public virtual LibraaryUser User { get; set; }

        [Required]
        public string BookId { get; set; }
        public virtual Book Book { get; set; }

        [Required]
        public DateTime IssuedOn { get; set; }
    }
}
