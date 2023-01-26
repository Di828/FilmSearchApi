namespace FilmSearch.Services.ActorService
{
    public interface IActorService
    {
        Task<ServiceResponse<GetActorDto>> GetSingleActor(int id);
        Task<ServiceResponse<List<GetActorDto>>> GetAllActors();
        Task<ServiceResponse<List<GetActorDto>>> DeleteActor(int id);
        Task<ServiceResponse<List<GetActorDto>>> AddActor(AddActorDto newActor);
        Task<ServiceResponse<GetActorDto>> UpdateActor(UpdateActorDto request);
    }
}
