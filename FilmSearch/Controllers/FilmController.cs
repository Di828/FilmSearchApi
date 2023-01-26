using FilmSearch.Services.FilmService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilmSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly IFilmService _filmService;

        public FilmController(IFilmService filmService) 
        {
            _filmService = filmService;
        }

        [HttpGet("All")]
        public async Task<ActionResult<ServiceResponse<List<GetFilmDto>>>> GetAllFilms()
        {
            var response = await _filmService.GetAllFilms();
            if (response.Data is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetFilmDto>>> GetSingleFilm(int id)
        {
            var response = await _filmService.GetSingleFilm(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetFilmDto>>>> AddFilm (AddFilmDto film)
        {
            var response = await _filmService.AddFilm(film);
            if (response.Data is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetFilmDto>>> UpdateFilm(UpdateFilmDto request)
        {
            var response = await _filmService.UpdateFilm(request);
            if (response.Data is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetFilmDto>>>> DeleteFilm(int id)
        {
            var response = await _filmService.DeleteFilm(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
