
namespace FilmSearch.Dtos.FilmD
{
    public class AddFilmDto
    {
        public string Title { get; set; } = string.Empty;
        public List<ActorInFilmDto>? Actors { get; set; }
        public List<ReviewInFilmDto>? Reviews { get; set; }
    }
}
