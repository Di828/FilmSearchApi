using FilmSearch.Services.FilmService;
using FilmSearch.Services.ReviewService;
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

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("All")]
        public async Task<ActionResult<ServiceResponse<List<GetReviewDto>>>> GetAllReviewsOnFilm(int filmId)
        {
            var response = await _reviewService.GetAllReviewsOnFilm(filmId);  
            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetReviewDto>>> GetSingleReview(int filmId, int id)
        {
            var response = await _reviewService.GetSingleReviewOnFilm(filmId, id);
            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetReviewDto>>>> AddReview(int filmId, AddReviewDto newReview)
        {

            var response = await _reviewService.AddReview(filmId, newReview);
            if (!response.Success)
            {
                return NotFound(response);
            }

            response = await _reviewService.GetAllReviewsOnFilm(filmId);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetFilmDto>>> UpdateFilm(int filmId, UpdateReviewDto request)
        {
            var response = await _reviewService.UpdateReview(filmId, request);
            if (response.Data is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetReviewDto>>>> DeleteReview(int filmId, int id)
        {
            var response = await _reviewService.DeleteReview(filmId, id);
            if (response.Data is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
