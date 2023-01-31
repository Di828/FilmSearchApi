using FilmSearch.Controllers;
using FilmSearch.Dtos.ActorD;
using FilmSearch.Dtos.ReviewD;
using FilmSearch.Models;
using FilmSearch.Services.ActorService;
using FilmSearch.Services.ReviewService;
using FilmSearch.Test.MockData;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmSearch.Test.Systems.Controllers
{
    public class TestReviewController
    {
        [Fact]
        public async Task GetAllReviews_ShouldReturn200Status()
        {
            /// Arrange
            int filmId = 1;
            var reviewService = new Mock<IReviewService>();
            reviewService.Setup(x => x.GetAllReviewsOnFilm(filmId)).ReturnsAsync(ReviewMockData.GetAllReviewOnFilmDto(filmId));
            var systemUnderTest = new ReviewController(reviewService.Object, new LoggerManager());

            /// Act
            var result = await systemUnderTest.GetAllReviewsOnFilm(filmId);


            /// Assert
            var actionResult = Assert.IsType<ActionResult<ServiceResponse<List<GetReviewDto>>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetAllReviews_ShouldReturn404Status()
        {
            /// Arrange
            int filmId = -1;
            var reviewService = new Mock<IReviewService>();
            reviewService.Setup(x => x.GetAllReviewsOnFilm(filmId)).ReturnsAsync(ReviewMockData.GetAllReviewOnEmptyFilmDto(filmId));
            var systemUnderTest = new ReviewController(reviewService.Object, new LoggerManager());

            /// Act
            var result = await systemUnderTest.GetAllReviewsOnFilm(filmId);


            /// Assert
            var actionResult = Assert.IsType<ActionResult<ServiceResponse<List<GetReviewDto>>>>(result);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task AddReviewCorrect_ShouldReturn200Status()
        {
            /// Arrange            
            int correctFilmId = 1;
            AddReviewDto anyCorrectReview = null;
            var reviewService = new Mock<IReviewService>();
            reviewService.Setup(x => x.AddReview(correctFilmId, anyCorrectReview)).ReturnsAsync(ReviewMockData.AddReviewCorrect(anyCorrectReview));
            var systemUnderTest = new ReviewController(reviewService.Object, new LoggerManager());

            /// Act
            var result = await systemUnderTest.AddReview(correctFilmId, anyCorrectReview);

            /// Assert
            var actionResult = Assert.IsType<ActionResult<ServiceResponse<List<GetReviewDto>>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task AddReviewIncorrect_ShouldReturn400Status()
        {
            /// Arrange            
            int incorrectFilmId = -1;
            AddReviewDto anyIncorrectReview = null;
            var reviewService = new Mock<IReviewService>();
            reviewService.Setup(x => x.AddReview(incorrectFilmId, anyIncorrectReview)).ReturnsAsync(ReviewMockData.AddReviewIncorrect(anyIncorrectReview));
            var systemUnderTest = new ReviewController(reviewService.Object, new LoggerManager());

            /// Act
            var result = await systemUnderTest.AddReview(incorrectFilmId, anyIncorrectReview);

            /// Assert
            var actionResult = Assert.IsType<ActionResult<ServiceResponse<List<GetReviewDto>>>>(result);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task DeleteReviewCorrect_ShouldReturn200Status()
        {
            /// Arrange    
            int filmId = 1;
            int id = 1;
            var reviewService = new Mock<IReviewService>();
            reviewService.Setup(x => x.DeleteReview(filmId, id)).ReturnsAsync(ReviewMockData.DeleteReviewCorrect(filmId, id));
            var systemUnderTest = new ReviewController(reviewService.Object, new LoggerManager());

            /// Act
            var result = await systemUnderTest.DeleteReview(filmId, id);

            /// Assert
            var actionResult = Assert.IsType<ActionResult<ServiceResponse<List<GetReviewDto>>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

    }
}
