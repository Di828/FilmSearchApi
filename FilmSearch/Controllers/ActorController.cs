using FilmSearch.Services.ActorService;
using FilmSearch.Services.FilmService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilmSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _actorService;

        public ActorController(IActorService actorService)
        {
            _actorService = actorService;
        }

        [HttpGet("All")]
        public async Task<ActionResult<ServiceResponse<List<GetActorDto>>>> GetAllActors()
        {
            var response = await _actorService.GetAllActors();
            if (response.Data is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetActorDto>>> GetSingleActor(int id)
        {
            var response = await _actorService.GetSingleActor(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetActorDto>>>> AddActor(AddActorDto newActor)
        {
            var response = await _actorService.AddActor(newActor);
            if (response.Data is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetActorDto>>> UpdateActor(UpdateActorDto request)
        {
            var response = await _actorService.UpdateActor(request);
            if (response.Data is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetActorDto>>>> DeleteFilm(int id)
        {
            var response = await _actorService.DeleteActor(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
