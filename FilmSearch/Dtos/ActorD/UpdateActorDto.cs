namespace FilmSearch.Dtos.ActorD
{
    public class UpdateActorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<int> FilmIds { get; set; }
    }
}
