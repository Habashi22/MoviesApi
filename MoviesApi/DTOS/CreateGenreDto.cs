namespace MoviesApi.DTOS
{
    public class CreateGenreDto
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

    }
}
