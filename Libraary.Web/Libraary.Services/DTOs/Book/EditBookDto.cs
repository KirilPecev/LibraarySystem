﻿namespace Libraary.Services.DTOs.Book
{
    using Microsoft.AspNetCore.Http;

    public class EditBookDto
    {
        public string Name { get; set; }

        public string Summary { get; set; }

        public string[] Authors { get; set; }

        public string Publisher { get; set; }

        public IFormFile NewPicture { get; set; }

        public string Picture { get; set; }

        public string[] Categories { get; set; }
    }
}
