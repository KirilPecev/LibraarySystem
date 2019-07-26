namespace Libraary.Domain
{
    using System.Collections.Generic;

    public class Rating : BaseModel<string>
    {
        public Rating()
        {
            this.Books = new HashSet<Book>();
        }

        public int CountOfScoresOne { get; set; }

        public int CountOfScoresTwo { get; set; }

        public int CountOfScoresThree { get; set; }

        public int CountOfScoresFour { get; set; }

        public int CountOfScoresFive { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
