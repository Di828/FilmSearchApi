using FilmSearch.Dtos.ActorD;
using FilmSearch.Dtos.ReviewD;
using FilmSearch.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmSearch.Test.MockData
{
    public class ReviewMockData
    {
        public static readonly List<Review> Reviews = new List<Review>()
        {
            new Review() {Id = 1, Title = "First review", Description = "Good review", FilmId = 1, Stars = 1, 
                ReviewedFilm = new Film(){Id = 1, Title = "First Film" } },
            new Review() {Id = 2, Title = "Second review", Description = "Good review", FilmId = 2, Stars = 2,
                ReviewedFilm = new Film(){Id = 2, Title = "Second Film" } },
            new Review() {Id = 3, Title = "Third review", Description = "Good review", FilmId = 3, Stars = 3,
                ReviewedFilm = new Film(){Id = 3, Title = "Third Film" } },
        };

        private static List<GetReviewDto> _reviewList = new List<GetReviewDto>
        {
            new GetReviewDto() {Id = 1, Title = "First review", Description = "Good review", Stars = 1},
            new GetReviewDto() {Id = 2, Title = "Second review", Description = "Good review", Stars = 2},
            new GetReviewDto() {Id = 3, Title = "Third review", Description = "Good review", Stars = 3},
        };

        public static ServiceResponse<List<GetReviewDto>> GetAllReviewOnFilmDto(int id = 1)
        {
            var response = new ServiceResponse<List<GetReviewDto>>();
            response.Data = new List<GetReviewDto>() { _reviewList[0] };

            return response;
        }

        public static ServiceResponse<List<GetReviewDto>> GetAllReviewOnEmptyFilmDto(int id = 1)
        {
            var response = new ServiceResponse<List<GetReviewDto>>();
            response.Success = false;

            return response;
        }

        public static ServiceResponse<List<GetReviewDto>> AddReviewCorrect(AddReviewDto newReview)
        {
            var response = new ServiceResponse<List<GetReviewDto>>();
            response.Data = _reviewList;

            return response;
        }

        public static ServiceResponse<List<GetReviewDto>> AddReviewIncorrect(AddReviewDto newReview)
        {
            var response = new ServiceResponse<List<GetReviewDto>>();
            response.Success = false;

            return response;
        }

        public static ServiceResponse<List<GetReviewDto>> DeleteReviewCorrect(int filmId, int id)
        {
            var response = new ServiceResponse<List<GetReviewDto>>();
            response.Data = _reviewList;

            return response;
        }
    }
}
