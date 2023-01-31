using FilmSearch.Data;
using FilmSearch.Dtos.ActorD;
using FilmSearch.Dtos.ReviewD;
using FilmSearch.Services.ActorService;
using FilmSearch.Services.ReviewService;
using FilmSearch.Test.MockData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmSearch.Test.Systems.Services
{
    public class TestReviewService : IDisposable
    {
        protected readonly DataContext _context;
        public TestReviewService()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(options);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetAllReviewsOnFilm_ReturnServiceResponseWithReviewCollection()
        {
            /// Arrange   
            int filmId = 2;
            int reviewsOnFilm = 1;
            _context.AddRange(ReviewMockData.Reviews);
            _context.SaveChanges();
            var sut = new ReviewService(_context);

            /// Act
            var result = await sut.GetAllReviewsOnFilm(filmId);

            /// Assert
            Assert.True(result != null);
            Assert.True(result.Success);
            Assert.Equal(reviewsOnFilm, result.Data.Count);
        }

        [Fact]
        public async Task GetAllReviewsOnFilm_ReturnServiceResponseWithNoDataAndSuccessIsFalse()
        {
            /// Arrange   
            int filmId = -1;            
            _context.AddRange(ReviewMockData.Reviews);
            _context.SaveChanges();
            var sut = new ReviewService(_context);

            /// Act
            var result = await sut.GetAllReviewsOnFilm(filmId);

            /// Assert
            Assert.True(result != null);
            Assert.False(result.Success);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task AddReview_ReturnServiceResponseWithReviewCollection()
        {
            /// Arrange   
            int filmId = 1;
            var correctReviewToAdd = new AddReviewDto() {Title = "Added Review", Description = "Any Desc", Stars = 3 };
            int reviewsOnFilmCount = 2;
            _context.AddRange(ReviewMockData.Reviews);
            _context.SaveChanges();
            var sut = new ReviewService(_context);

            /// Act
            var result = await sut.AddReview(filmId, correctReviewToAdd);

            /// Assert
            Assert.True(result != null);
            Assert.True(result.Success);
            Assert.Equal(reviewsOnFilmCount, result.Data.Count);
        }

        [Fact]
        public async Task AddReviewIncorrect_ReturnServiceResponseWithNoDataSuccessIsFalse()
        {
            /// Arrange   
            int filmId = -1;
            var correctReviewToAdd = new AddReviewDto() { Title = "Added Review", Description = "Any Desc", Stars = 3 };            
            _context.AddRange(ReviewMockData.Reviews);
            _context.SaveChanges();
            var sut = new ReviewService(_context);

            /// Act
            var result = await sut.AddReview(filmId, correctReviewToAdd);

            /// Assert
            Assert.True(result != null);
            Assert.False(result.Success);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task UpdateReview_ReturnServiceResponseWithGetReviewDto()
        {
            /// Arrange   
            int filmId = 1;
            var correctReviewForUpdate = new UpdateReviewDto() { Id = 1, Title = "UpdatedTitle", Description = "Desc", Stars = 2 };
            _context.AddRange(ReviewMockData.Reviews);
            _context.SaveChanges();
            var sut = new ReviewService(_context);

            /// Act
            var result = await sut.UpdateReview(filmId, correctReviewForUpdate);

            /// Assert
            Assert.True(result != null);
            Assert.True(result.Success);
            Assert.Equal(ReviewMockData.Reviews[0].Title, result.Data.Title);
        }

        [Fact]
        public async Task DeleteReviewIncorrect_ReturnServiceResponseSucsessFalseDataNull()
        {
            /// Arrange   
            int reviewId = -1;
            int filmId = 1;
            _context.AddRange(ReviewMockData.Reviews);
            _context.SaveChanges();
            var sut = new ReviewService(_context);

            /// Act
            var result = await sut.DeleteReview(filmId, reviewId);

            /// Assert
            Assert.True(result != null);
            Assert.False(result.Success);
            Assert.Null(result.Data);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
