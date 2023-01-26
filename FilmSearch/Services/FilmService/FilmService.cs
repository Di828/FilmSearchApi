
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using FilmSearch.Services.FilmServiceExtensions;
using Microsoft.EntityFrameworkCore;

namespace FilmSearch.Services.FilmService
{
    public class FilmService : IFilmService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public FilmService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetFilmDto>>> GetAllFilms()
        {
            var serviceResponse = new ServiceResponse<List<GetFilmDto>>();
            var dbFilms = await _context.Films
                .Include(c => c.Actors)
                .ToListAsync();
            serviceResponse.Data = CreateResponse(dbFilms);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetFilmDto>> GetSingleFilm(int id)
        {
            var serviceResponse = new ServiceResponse<GetFilmDto>();
            var dbFilm = await _context.Films
                .Include(c => c.Actors)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (dbFilm is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "There is no movie with this id.";
                return serviceResponse;
            }

            serviceResponse.Data = CreateSingleResponse(dbFilm);

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetFilmDto>>> AddFilm(AddFilmDto newFilm)
        {            
            var serviceResponse = new ServiceResponse<List<GetFilmDto>>();

            if (string.IsNullOrEmpty(newFilm.Title))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Film Title can not be null or empty";
                return serviceResponse;
            }

            var film = await CreateFilmModel(newFilm);            
            if (!film.UpdateActors())
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Each actor must got a firstname and a lastname";
                return serviceResponse;
            }

            film.UpdateReviews();

            _context.Films.Add(film);
            await _context.SaveChangesAsync();
            serviceResponse.Data = CreateResponse(await _context.Films.Include(c => c.Actors).ToListAsync());

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetFilmDto>> UpdateFilm(UpdateFilmDto request)
        {
            var serviceResponse = new ServiceResponse<GetFilmDto>();
            if (string.IsNullOrEmpty(request.Title))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "You can't replace title on empty string";
                return serviceResponse;
            }

            var film = await _context.Films
                .Include(c => c.Actors)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            if (film is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "There is no movie with this id.";
                return serviceResponse;
            }
            else
            {
                film.Title = request.Title;
                await _context.SaveChangesAsync();
            }

            serviceResponse.Data = CreateSingleResponse(film);

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetFilmDto>>> DeleteFilm(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetFilmDto>>();            
            var film = await _context.Films.FirstOrDefaultAsync(x => x.Id == id);
            if (film is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "There is no movie with this id.";
                return serviceResponse;
            }
            else
            {
                _context.Films.Remove(film);
                await _context.SaveChangesAsync();                
            }

            serviceResponse.Data = CreateResponse(_context.Films.Include(c => c.Actors).ToList());

            return serviceResponse;
        }

        private List<GetFilmDto> CreateResponse(List<Film> films)
        {
            List<GetFilmDto> response = new List<GetFilmDto>();
            foreach (var film in films) 
            {
                response.Add(CreateSingleResponse(film));
            }

            return response;
        }

        private GetFilmDto CreateSingleResponse(Film film)
        {
            GetFilmDto responseDto = new GetFilmDto();
            responseDto.Id = film.Id;
            responseDto.Title = film.Title;
            var reviews = _context.Reviews.Where(x => x.FilmId == film.Id).ToList();
            if (film.Actors is not null)
            {
                responseDto.Actors = film.Actors.Select(x => _mapper.Map<ActorInFilmDto>(x)).ToList();
            }

            if (reviews is not null)
            {
                responseDto.Reviews = reviews.Select(x => _mapper.Map<ReviewInFilmDto>(x)).ToList();
            }

            return responseDto;
        }

        private async Task<Film> CreateFilmModel(AddFilmDto filmToAdd)
        {
            Film output = new Film();
            output.Title = filmToAdd.Title;
            if (filmToAdd.Actors is not null)
            {
                output.Actors = new List<Actor>();

                foreach (var actor in filmToAdd.Actors)
                {
                    var findActor = await _context.Actors.FirstOrDefaultAsync(x => x.FirstName == actor.FirstName && x.LastName == actor.LastName);
                    if (findActor is not null)
                    {
                        output.Actors.Add(findActor);
                        continue;
                    }

                    Actor newActor = new Actor();
                    newActor.FirstName = actor.FirstName;
                    newActor.LastName = actor.LastName;
                    output.Actors.Add(newActor);
                }
            }

            if (filmToAdd.Reviews is not null)
            {
                output.Reviews = new List<Review>();

                foreach (var review in filmToAdd.Reviews)
                {
                    Review newReview = new Review();
                    newReview.Title = review.Title;
                    newReview.Description = review.Description;
                    newReview.Stars = review.Stars;
                    output.Reviews.Add(newReview);
                }
            }

            return output;
        }
    }
}
