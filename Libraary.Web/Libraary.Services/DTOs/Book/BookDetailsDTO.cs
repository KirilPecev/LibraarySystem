﻿namespace Libraary.Services.DTOs.Book
{
    public class BookDetailsDTO
    {
        public string Name { get; set; }

        public string Summary { get; set; }

        public decimal Fee { get; set; }

        public string Author { get; set; }

        public bool IsRented { get; set; }

        public string Publisher { get; set; }

        public int Rating { get; set; }

        public string Picture { get; set; }

        public string Categories { get; set; }
    }
}
