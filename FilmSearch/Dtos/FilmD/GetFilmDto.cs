namespace FilmSearch.Dtos.FilmD
{
    public class GetFilmDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<ActorInFilmDto>? Actors { get; set; }
        public List<ReviewInFilmDto>? Reviews { get; set; }
    }
}