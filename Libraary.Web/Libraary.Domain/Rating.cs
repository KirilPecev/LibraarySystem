namespace Libraary.Domain
{
    using System.ComponentModel.DataAnnotations;

    public class Rating : BaseModel<string>
    {
        [Required]
        public string BookId { get; set; }
        public Book Book { get; set; }

        public int CountOfScoresOne { get; set; }

        public int CountOfScoresTwo { get; set; }

        public int CountOfScoresThree { get; set; }

        public int CountOfScoresFour { get; set; }

        public int CountOfScoresFive { get; set; }
    }
}
