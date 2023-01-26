using FilmSearch.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmSearch.Services.ReviewService
{
    public class ReviewService : IReviewService
    {
        private readonly DataContext _context;
        public ReviewService(DataContext context)
        {            
            _context = context;
        }

        public async Task<ServiceResponse<List<GetReviewDto>>> GetAllReviewsOnFilm(int filmId)
        {
            ServiceResponse<List<GetReviewDto>> serviceResponse = new ServiceResponse<List<GetReviewDto>>();
            var reviews = await _context.Reviews.Where(x => x.FilmId == filmId).ToListAsync();
            if (reviews.Count == 0)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "There is no movie with this id.";
                return serviceResponse;
            }

            serviceResponse.Data = await CreateResponse(reviews);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetReviewDto>> GetSingleReviewOnFilm(int filmId, int id)
        {
            ServiceResponse<GetReviewDto> serviceResponse = new ServiceResponse<GetReviewDto>();
            var dbReview = await _context.Reviews.ToListAsync();
            var review = dbReview.Find(x => x.Id == id);
            if (review is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "There is no review with this id.";
                return serviceResponse;
            }
            else
            {
                if (review.FilmId != filmId)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "There is a review but not for this movie.";
                    return serviceResponse;
                }

                serviceResponse.Data = await CreateSingleResponse(review);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetReviewDto>>> AddReview(int filmId, AddReviewDto newReview)
        {
            var serviceResponse = new ServiceResponse<List<GetReviewDto>>();
            var film = await _context.Films.FirstOrDefaultAsync(x => x.Id == filmId);
            if (film is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "There is no movie with this id.";
                return serviceResponse;
            }

            var review = new Review
            {
                FilmId = filmId,
                Description = newReview.Description,
                Stars = newReview.Stars,
                Title = newReview.Title,
                ReviewedFilm = film
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            var reviews = await _context.Reviews.Where(x => x.FilmId == filmId).ToListAsync();

            serviceResponse.Data = await CreateResponse(reviews);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetReviewDto>> UpdateReview(int filmId, UpdateReviewDto request)
        {
            var serviceResponse = new ServiceResponse<GetReviewDto>();
            var film = await _context.Films.FirstOrDefaultAsync(x => x.Id == filmId);
            if (film is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "There is no movie with this id.";
                return serviceResponse;
            }

            var review = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (review is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "There is no review with this id.";
                return serviceResponse;
            }
            else
            {
                UpdateReview(review, request);
                await _context.SaveChangesAsync();
            }

            serviceResponse.Data = await CreateSingleResponse(review);

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetReviewDto>>> DeleteReview(int filmId, int id)
        {
            var serviceResponse = new ServiceResponse<List<GetReviewDto>>();            
            var film = await _context.Films.FirstOrDefaultAsync(x => x.Id == filmId);
            if (film is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "There is no movie with this id.";
                return serviceResponse;
            }

            var review = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id);
            if (review is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "There is no review with this id.";
                return serviceResponse;
            }
            else
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }            

            serviceResponse.Data = await CreateResponse(await _context.Reviews.Where(x => x.FilmId == filmId).ToListAsync());

            return serviceResponse;
        }

        private static void UpdateReview(Review review, UpdateReviewDto request)
        {
            review.Description = request.Description;
            review.Stars = request.Stars;
            review.Title = request.Title;
        }

        private async Task<GetReviewDto> CreateSingleResponse(Review review)
        {
            GetReviewDto output = new GetReviewDto();
            output.Id = review.Id;
            output.Stars = review.Stars;
            output.Description = review.Description;
            output.Title = review.Title;
            var film = await _context.Films.FirstOrDefaultAsync(x => x.Id == review.FilmId);
            output.FilmTitle = film.Title;

            return output;
        }

        private async Task<List<GetReviewDto>> CreateResponse(List<Review> reviews)
        {
            List<GetReviewDto> response = new List<GetReviewDto>();
            foreach (var review in reviews)
            {
                response.Add(await CreateSingleResponse(review));
            }

            return response;
        }
    }
}
