namespace FilmSearch.Services.ReviewService
{
    public interface IReviewService
    {
        Task<ServiceResponse<List<GetReviewDto>>> GetAllReviewsOnFilm(int filmId);
        Task<ServiceResponse<GetReviewDto>> GetSingleReviewOnFilm(int filmId, int id);

        Task<ServiceResponse<List<GetReviewDto>>> AddReview(int filmId, AddReviewDto newReview);
        Task<ServiceResponse<GetReviewDto>> UpdateReview(int filmId, UpdateReviewDto request);
        Task<ServiceResponse<List<GetReviewDto>>> DeleteReview(int filmId, int id);
    }
}
