namespace MoviesApi.DTOS
{
    public class MovieDto
    {

        public String Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }

        public String StoreLine { get; set; }

        public IFormFile? Poster { get; set; }


        public int genreId { get; set; }

    }
}
