using FilmSearch.Services.FilmService;
using FilmSearch.Services.ReviewService;
using LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace FilmSearch.Controllers
{
    [Route("api/film/{filmId}/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {

        private readonly IReviewService _reviewService;
        private readonly ILoggerManager _logger;

        public ReviewController(IReviewService reviewService, ILoggerManager loger)
        {
            _reviewService = reviewService;
            _logger = loger;
        }

        [HttpGet("All")]
        public async Task<ActionResult<ServiceResponse<List<GetReviewDto>>>> GetAllReviewsOnFilm(int filmId)
        {
            _logger.LogInfo("Fetching all reviews from the storage");
            var response = await _reviewService.GetAllReviewsOnFilm(filmId);  
            if (response.Data is null)
            {
                _logger.LogInfo($"Reterning not found response");

                return NotFound(response);
            }

            _logger.LogInfo($"Reterning {response.Data.Count} reviews");

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetReviewDto>>> GetSingleReview(int filmId, int id)
        {
            _logger.LogInfo($"Fetching review with id: {id} from the storage");
            var response = await _reviewService.GetSingleReviewOnFilm(filmId, id);
            if (response.Data is null)
            {
                _logger.LogInfo($"Reterning not found response");

                return NotFound(response);
            }

            _logger.LogInfo($"Reterning review with id:{id}");

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetReviewDto>>>> AddReview(int filmId, AddReviewDto newReview)
        {
            _logger.LogInfo($"Adding review to storage");
            var response = await _reviewService.AddReview(filmId, newReview);
            if (response.Data is null)
            {
                _logger.LogInfo("Reterning bad request response");

                return BadRequest(response);
            }

            _logger.LogInfo($"Reterning {response.Data.Count} reviews");

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetReviewDto>>> UpdateReview(int filmId, UpdateReviewDto request)
        {
            _logger.LogInfo($"Updaiting review in storage");
            var response = await _reviewService.UpdateReview(filmId, request);
            if (response.Data is null)
            {
                _logger.LogInfo("Reterning bad request response");

                return BadRequest(response);
            }

            _logger.LogInfo($"Reterning updated review");

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetReviewDto>>>> DeleteReview(int filmId, int id)
        {
            _logger.LogInfo($"Deleting review from storage");
            var response = await _reviewService.DeleteReview(filmId, id);
            if (response.Data is null)
            {
                _logger.LogInfo("Reterning not found response");

                return NotFound(response);
            }

            _logger.LogInfo($"Reterning {response.Data.Count} reviews");

            return Ok(response);
        }
    }
}
