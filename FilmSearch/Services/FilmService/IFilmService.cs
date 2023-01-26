
namespace FilmSearch.Services.FilmService
{
    public interface IFilmService
    {
        Task<ServiceResponse<List<GetFilmDto>>> GetAllFilms();
        Task<ServiceResponse<GetFilmDto>> GetSingleFilm(int id);

        Task<ServiceResponse<List<GetFilmDto>>> AddFilm(AddFilmDto newFilm);
        Task<ServiceResponse<GetFilmDto>> UpdateFilm(UpdateFilmDto request);
        Task<ServiceResponse<List<GetFilmDto>>> DeleteFilm(int id);
    }
}