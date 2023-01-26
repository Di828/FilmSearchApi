using FilmSearch.Services.FilmService;
using LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilmSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly IFilmService _filmService;
        private readonly ILoggerManager _logger;

        public FilmController(IFilmService filmService, ILoggerManager logger) 
        {
            _filmService = filmService;
            _logger = logger;
        }

        [HttpGet("All")]
        public async Task<ActionResult<ServiceResponse<List<GetFilmDto>>>> GetAllFilms()
        {
            _logger.LogInfo("Fetching all films from the storage");
            var response = await _filmService.GetAllFilms();
            if (response.Data is null)
            {
                _logger.LogInfo($"Reterning not found response");

                return NotFound(response);
            }

            _logger.LogInfo($"Reterning {response.Data.Count} films");

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetFilmDto>>> GetSingleFilm(int id)
        {
            _logger.LogInfo($"Fetching film with id: {id} from the storage");
            var response = await _filmService.GetSingleFilm(id);
            if (response.Data is null)
            {
                _logger.LogInfo($"Reterning not found response");

                return NotFound(response);
            }

            _logger.LogInfo($"Reterning film with id:{id}");

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetFilmDto>>>> AddFilm (AddFilmDto film)
        {
            _logger.LogInfo($"Adding film to storage");
            var response = await _filmService.AddFilm(film);
            if (response.Data is null)
            {
                _logger.LogInfo("Reterning bad request response");

                return BadRequest(response);
            }

            _logger.LogInfo($"Reterning {response.Data.Count} films");

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetFilmDto>>> UpdateFilm(UpdateFilmDto request)
        {
            _logger.LogInfo($"Updaiting film in storage");
            var response = await _filmService.UpdateFilm(request);
            if (response.Data is null)
            {
                _logger.LogInfo("Reterning bad request response");

                return BadRequest(response);
            }

            _logger.LogInfo($"Reterning updated film");

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetFilmDto>>>> DeleteFilm(int id)
        {
            _logger.LogInfo($"Deleting film from storage");
            var response = await _filmService.DeleteFilm(id);
            if (response.Data is null)
            {
                _logger.LogInfo("Reterning not found response");

                return NotFound(response);
            }

            _logger.LogInfo($"Reterning {response.Data.Count} films");

            return Ok(response);
        }
    }
}
