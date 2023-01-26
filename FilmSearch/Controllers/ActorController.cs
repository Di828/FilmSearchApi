using FilmSearch.Services.ActorService;
using FilmSearch.Services.FilmService;
using LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilmSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _actorService;
        private readonly ILoggerManager _logger;

        public ActorController(IActorService actorService, ILoggerManager logger)
        {
            _actorService = actorService;
            _logger = logger; 
        }

        [HttpGet("All")]
        public async Task<ActionResult<ServiceResponse<List<GetActorDto>>>> GetAllActors()
        {
            _logger.LogInfo("Fetching all the actors from the storage");
            var response = await _actorService.GetAllActors();
            if (response.Data is null)
            {
                _logger.LogInfo($"Reterning not found response");

                return NotFound(response);
            }

            _logger.LogInfo($"Reterning {response.Data.Count} actors");

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetActorDto>>> GetSingleActor(int id)
        {
            _logger.LogInfo($"Fetching actor with id: {id} from the storage");
            var response = await _actorService.GetSingleActor(id);
            if (response.Data is null)
            {
                _logger.LogInfo($"Reterning not found response");

                return NotFound(response);
            }

            _logger.LogInfo($"Reterning actor with id:{id}");

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetActorDto>>>> AddActor(AddActorDto newActor)
        {
            _logger.LogInfo($"Adding actor to storage");
            var response = await _actorService.AddActor(newActor);            
            if (response.Data is null)
            {
                _logger.LogInfo("Reterning bad request response");

                return BadRequest(response);
            }

            _logger.LogInfo($"Reterning {response.Data.Count} actors");

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetActorDto>>> UpdateActor(UpdateActorDto request)
        {
            _logger.LogInfo($"Updaiting actor in storage");
            var response = await _actorService.UpdateActor(request);
            if (response.Data is null)
            {
                _logger.LogInfo("Reterning bad request response");

                return BadRequest(response);
            }

            _logger.LogInfo($"Reterning updated actor");

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetActorDto>>>> DeleteActor(int id)
        {
            _logger.LogInfo($"Deleting actor from storage");
            var response = await _actorService.DeleteActor(id);
            if (response.Data is null)
            {
                _logger.LogInfo("Reterning not found response");

                return NotFound(response);
            }

            _logger.LogInfo($"Reterning {response.Data.Count} actors");

            return Ok(response);
        }
    }
}
