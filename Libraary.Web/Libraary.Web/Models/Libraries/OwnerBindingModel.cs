﻿namespace Libraary.Web.Models.Libraries
{
    using System.ComponentModel.DataAnnotations;

    public class OwnerBindingModel
    {
        public string LibraryId { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
