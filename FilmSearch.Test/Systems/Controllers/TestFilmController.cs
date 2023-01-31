using FilmSearch.Controllers;
using FilmSearch.Dtos.ActorD;
using FilmSearch.Dtos.FilmD;
using FilmSearch.Models;
using FilmSearch.Services.ActorService;
using FilmSearch.Services.FilmService;
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
    public class TestFilmController
    {
        [Fact]
        public async Task GetAllFilms_ShouldReturn200Status()
        {
            /// Arrange
            var filmService = new Mock<IFilmService>();
            filmService.Setup(x => x.GetAllFilms()).ReturnsAsync(FilmMockData.GetAllFilms());
            var systemUnderTest = new FilmController(filmService.Object, new LoggerManager());

            /// Act
            var result = await systemUnderTest.GetAllFilms();


            /// Assert
            var actionResult = Assert.IsType<ActionResult<ServiceResponse<List<GetFilmDto>>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetSingleFilm_ShouldReturn200Status()
        {
            /// Arrange
            int id = 1;
            var filmService = new Mock<IFilmService>();
            filmService.Setup(x => x.GetSingleFilm(id)).ReturnsAsync(FilmMockData.GetSingleFilmCorrect(id));
            var systemUnderTest = new FilmController(filmService.Object, new LoggerManager());

            /// Act
            var result = await systemUnderTest.GetSingleFilm(id);


            /// Assert
            var actionResult = Assert.IsType<ActionResult<ServiceResponse<GetFilmDto>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetSingleFilm_ShouldReturn404Status()
        {
            /// Arrange
            int id = -1;
            var filmService = new Mock<IFilmService>();
            filmService.Setup(x => x.GetSingleFilm(id)).ReturnsAsync(FilmMockData.GetSingleFilmIncorrect(id));
            var systemUnderTest = new FilmController(filmService.Object, new LoggerManager());

            /// Act
            var result = await systemUnderTest.GetSingleFilm(id);


            /// Assert
            var actionResult = Assert.IsType<ActionResult<ServiceResponse<GetFilmDto>>>(result);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task AddFilmCorrect_ShouldReturn200Status()
        {
            /// Arrange            
            var filmService = new Mock<IFilmService>();
            filmService.Setup(x => x.AddFilm(null)).ReturnsAsync(FilmMockData.AddFilmCorrect(null));
            var syt = new FilmController(filmService.Object, new LoggerManager());

            /// Act
            var result = await syt.AddFilm(null);

            /// Assert
            var actionResult = Assert.IsType<ActionResult<ServiceResponse<List<GetFilmDto>>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task AddFilmIncorrect_ShouldReturn400Status()
        {
            /// Arrange            
            var filmService = new Mock<IFilmService>();
            filmService.Setup(x => x.AddFilm(null)).ReturnsAsync(FilmMockData.AddFilmIncorrect(null));
            var syt = new FilmController(filmService.Object, new LoggerManager());

            /// Act
            var result = await syt.AddFilm(null);

            /// Assert
            var actionResult = Assert.IsType<ActionResult<ServiceResponse<List<GetFilmDto>>>>(result);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task DeleteFilmCorrect_ShouldReturn200Status()
        {
            /// Arrange    
            int id = 3;
            var filmService = new Mock<IFilmService>();
            filmService.Setup(x => x.DeleteFilm(id)).ReturnsAsync(FilmMockData.DeleteFilmCorrect(id));
            var systemUnderTest = new FilmController(filmService.Object, new LoggerManager());

            /// Act
            var result = await systemUnderTest.DeleteFilm(id);

            /// Assert
            var actionResult = Assert.IsType<ActionResult<ServiceResponse<List<GetFilmDto>>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task DeleteFilmCorrect_ShouldReturn400Status()
        {
            /// Arrange    
            int id = -1;
            var filmService = new Mock<IFilmService>();
            filmService.Setup(x => x.DeleteFilm(id)).ReturnsAsync(FilmMockData.DeleteFilmIncorrect(id));
            var systemUnderTest = new FilmController(filmService.Object, new LoggerManager());

            /// Act
            var result = await systemUnderTest.DeleteFilm(id);

            /// Assert
            var actionResult = Assert.IsType<ActionResult<ServiceResponse<List<GetFilmDto>>>>(result);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        }

    }
}
