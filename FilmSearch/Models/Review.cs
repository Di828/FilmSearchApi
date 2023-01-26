namespace FilmSearch.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Stars { get; set; }
        public Film? ReviewedFilm { get; set; }
        public int FilmId { get; set; }
    }
}