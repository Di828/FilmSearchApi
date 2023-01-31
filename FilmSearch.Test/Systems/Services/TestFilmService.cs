using AutoMapper;
using FilmSearch.Data;
using FilmSearch.Dtos.ActorD;
using FilmSearch.Dtos.FilmD;
using FilmSearch.Services.ActorService;
using FilmSearch.Services.FilmService;
using FilmSearch.Test.MockData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmSearch.Test.Systems.Services
{
    public class TestFilmService : IDisposable
    {
        protected readonly DataContext _context;
        private readonly IMapper _mapper;
        public TestFilmService()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(options);            

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetAllFilms_ReturnServiceResponseWithFilmCollection()
        {
            /// Arrange   
            _context.AddRange(FilmMockData.Films);
            _context.SaveChanges();
            var sut = new FilmService(_mapper, _context);

            /// Act
            var result = await sut.GetAllFilms();

            /// Assert
            Assert.True(result != null);
            Assert.True(result.Success);
            Assert.Equal(ActorMockData.Actors.Count, result.Data.Count);
        }

        [Fact]
        public async Task GetSingleFilm_ReturnServiceResponseWithFilm()
        {
            /// Arrange   
            int filmId = 1;
            int filmIndex = 0;
            _context.AddRange(FilmMockData.Films);
            _context.SaveChanges();
            var sut = new FilmService(_mapper, _context);

            /// Act
            var result = await sut.GetSingleFilm(filmId);

            /// Assert
            Assert.True(result != null);
            Assert.True(result.Success);
            Assert.Equal(FilmMockData.Films[filmIndex].Title, result.Data.Title);
        }

        [Fact]
        public async Task GetSingleAbsentFilm_ReturnServiceResponseWithNoDataAndSuccsessIsFalse()
        {
            /// Arrange   
            int filmId = -1;
            _context.AddRange(FilmMockData.Films);
            _context.SaveChanges();
            var sut = new FilmService(_mapper, _context);

            /// Act
            var result = await sut.GetSingleFilm(filmId);

            /// Assert
            Assert.True(result != null);
            Assert.False(result.Success);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task AddFilm_ReturnServiceResponseWithFilmCollection()
        {
            /// Arrange   
            _context.AddRange(FilmMockData.Films);
            _context.SaveChanges();
            var sut = new FilmService(_mapper, _context);

            /// Act
            var result = await sut.AddFilm(new AddFilmDto() {Title = "forth film" } );

            /// Assert
            Assert.True(result != null);
            Assert.True(result.Success);
            Assert.Equal(FilmMockData.Films.Count + 1, result.Data.Count);
        }

        [Fact]
        public async Task UpdateFilm_ReturnServiceResponseWithGetFilmDto()
        {
            /// Arrange   
            _context.AddRange(FilmMockData.Films);
            _context.SaveChanges();
            var sut = new FilmService(_mapper, _context);

            /// Act
            var result = await sut.UpdateFilm(new UpdateFilmDto() {Id = 1, Title = "FirstUpdateFilm"});

            /// Assert
            Assert.True(result != null);
            Assert.True(result.Success);
            Assert.Equal(FilmMockData.Films[0].Title, result.Data.Title);
        }

        [Fact]
        public async Task DeleteFilm_ReturnServiceResponseWithFilmCollection()
        {
            /// Arrange   
            int filmId = 1;
            _context.AddRange(FilmMockData.Films);
            _context.SaveChanges();
            var sut = new FilmService(_mapper, _context);

            /// Act
            var result = await sut.DeleteFilm(filmId);

            /// Assert
            Assert.True(result != null);
            Assert.True(result.Success);
            Assert.Equal(FilmMockData.Films.Count - 1, result.Data.Count);
        }

        [Fact]
        public async Task DeleteFilmIncorrectId_ReturnServiceResponseSucsessFalseDataNull()
        {
            /// Arrange   
            int filmId = -1;
            _context.AddRange(FilmMockData.Films);
            _context.SaveChanges();
            var sut = new FilmService(_mapper, _context);

            /// Act
            var result = await sut.DeleteFilm(filmId);

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
