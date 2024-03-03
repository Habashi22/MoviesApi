namespace MoviesApi.Models
{
    public class Movie
    {

        public int Id { get; set; }
        public String Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }

        public String StoreLine { get; set; }

        public byte[] Poster { get; set; }


        public int genreId { get; set; }
        public genre genre { get; set; }

    }
}
