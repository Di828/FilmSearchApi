using Microsoft.EntityFrameworkCore;

namespace FilmSearch.Services.ActorService
{
    public class ActorService : IActorService
    {
        private readonly DataContext _context;
        public ActorService(DataContext context) 
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<GetActorDto>>> GetAllActors()
        {
            var serviceResponse = new ServiceResponse<List<GetActorDto>>();
            var dbActors = await _context.Actors
                .Include(c => c.Films)
                .ToListAsync();
            serviceResponse.Data = CreateResponse(dbActors);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetActorDto>> GetSingleActor(int id)
        {
            var serviceResponse = new ServiceResponse<GetActorDto>();
            var dbActor = await _context.Actors
                .Include(c => c.Films)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (dbActor is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "There is no actor with this id.";
                return serviceResponse;
            }

            serviceResponse.Data = CreateSingleResponse(dbActor);

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetActorDto>>> AddActor(AddActorDto newActor)
        {
            var serviceResponse = new ServiceResponse<List<GetActorDto>>();
            if (!(await MovieDataCorrect(newActor.FilmIds)))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "There is no movie with one or more ids that you send or movie list is empty.";
                return serviceResponse;
            }

            var actor = await CreateActorModel(newActor);
            if (!IsValidActorData(actor))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Actor must have a first name and a lastname.";
                return serviceResponse;
            }

            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();
            serviceResponse.Data = CreateResponse(await _context.Actors
                .Include(c => c.Films)
                .ToListAsync());

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetActorDto>> UpdateActor(UpdateActorDto request)
        {
            var serviceResponse = new ServiceResponse<GetActorDto>();
            if (!(await MovieDataCorrect(request.FilmIds)))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "There is no movie with one or more ids that you send or movie list is empty.";
                return serviceResponse;
            }

            var actor = await _context.Actors.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (actor is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "There is no actor with this id.";
                return serviceResponse;
            }

            actor.FirstName = request.FirstName;
            actor.LastName = request.LastName;
            actor.Films = await AddFilmsToActor(request.FilmIds);
            if (!IsValidActorData(actor))
            { 
                serviceResponse.Success = false;
                serviceResponse.Message = "Actor must have a first name and a lastname.";
                return serviceResponse;
            }

            await _context.SaveChangesAsync();
            serviceResponse.Data = CreateSingleResponse(actor);

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetActorDto>>> DeleteActor(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetActorDto>>();
            var actorDb = await _context.Actors.SingleOrDefaultAsync(x => x.Id == id);
            if (actorDb is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "There is no actor with this id.";
                return serviceResponse;
            }
            else
            {
                _context.Actors.Remove(actorDb);
                await _context.SaveChangesAsync();                
            }

            serviceResponse.Data = CreateResponse(await _context.Actors
                .Include(c => c.Films)
                .ToListAsync());

            return serviceResponse;
        }

        private List<GetActorDto> CreateResponse(List<Actor> actors)
        {
            List<GetActorDto> response = new List<GetActorDto>();
            foreach (var actor in actors)
            {
                response.Add(CreateSingleResponse(actor));
            }

            return response;
        }

        private async Task<bool> MovieDataCorrect(List<int> filmsIds)
        {
            if (filmsIds.Count == 0)
            {
                return false;
            }

            foreach (var filmId in filmsIds)
            {
                var film = await _context.Films.FirstOrDefaultAsync(x => x.Id == filmId);
                if (film is null)
                {
                    return false;
                }
            }

            return true;
        }

        private GetActorDto CreateSingleResponse(Actor actor)
        {
            GetActorDto responseDto = new GetActorDto();
            responseDto.Id = actor.Id;
            responseDto.FirstName = actor.FirstName;
            responseDto.LastName = actor.LastName;
            if (actor.Films is not null)
            {
                responseDto.Films = actor.Films.Select(x => x.Title).ToList();
            }

            return responseDto;
        }

        private async Task<Actor> CreateActorModel(AddActorDto actorToAdd)
        {
            Actor output = new Actor();
            output.FirstName = actorToAdd.FirstName;
            output.LastName = actorToAdd.LastName;
            output.Films = await AddFilmsToActor(actorToAdd.FilmIds);

            return output;
        }

        private async Task<List<Film>> AddFilmsToActor(List<int> filmsIds)
        {
            var output = new List<Film>();
            foreach (var filmId in filmsIds)
            {
                var film = await _context.Films
                    .Include(c => c.Actors)
                    .Include(c => c.Reviews)
                    .FirstOrDefaultAsync(x => x.Id == filmId);
                output.Add(film);
            }

            return output;
        }

        private bool IsValidActorData(Actor actor)
        {
            if (string.IsNullOrEmpty(actor.FirstName))
            {
                return false;
            }

            if (string.IsNullOrEmpty(actor.LastName))
            {
                return false;
            }

            return true;
        }
    }
}
