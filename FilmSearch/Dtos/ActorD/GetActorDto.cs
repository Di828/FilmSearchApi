namespace FilmSearch.Dtos.ActorD
{
    public class GetActorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public List<string>? Films { get; set; }
    }
}
