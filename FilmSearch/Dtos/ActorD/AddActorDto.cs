namespace FilmSearch.Dtos.ActorD
{
    public class AddActorDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public List<int> FilmIds { get; set; }
    }
}
