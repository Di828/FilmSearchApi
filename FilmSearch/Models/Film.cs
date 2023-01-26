namespace FilmSearch.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<Actor>? Actors { get; set; }
        public List<Review>? Reviews { get; set; }
    }
}
