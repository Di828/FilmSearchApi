using FilmSearch.Controllers;
using FilmSearch.Dtos.ActorD;
using FilmSearch.Models;
using FilmSearch.Services.ActorService;
using FilmSearch.Test.MockData;
using FluentAssertions;
using LoggerService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmSearch.Test.Systems.Controllers
{
    public class TestActorController
    {
        [Fact]
        public async Task GetAllActors_ShouldReturn200Status()
        {
            /// Arrange
            var actorService = new Mock<IActorService>();
            actorService.Setup(x => x.GetAllActors()).ReturnsAsync(ActorMockData.GetAllActorsDto());            
            var systemUnderTest = new ActorController(actorService.Object, new LoggerManager());

            /// Act
            var result = await systemUnderTest.GetAllActors();


            /// Assert
            var actionResult = Assert.IsType<ActionResult<ServiceResponse<List<GetActorDto>>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetAllActors_ShouldReturn404Status()
        {
            /// Arrange
            var actorService = new Mock<IActorService>();
            actorService.Setup(x => x.GetAllActors()).ReturnsAsync(ActorMockData.GetAllActorsEmptyDto());
            var systemUnderTest = new ActorController(actorService.Object, new LoggerManager());

            /// Act
            var result = await systemUnderTest.GetAllActors();


            /// Assert
            var actionResult = Assert.IsType<ActionResult<ServiceResponse<List<GetActorDto>>>>(result);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetSingleActor_ShouldReturn200Status()
        {
            /// Arrange
            int id = 1;
            var actorService = new Mock<IActorService>();
            actorService.Setup(x => x.GetSingleActor(id)).ReturnsAsync(ActorMockData.GetSingleActorCorrect(id));
            var systemUnderTest = new ActorController(actorService.Object, new LoggerManager());

            /// Act
            var result = await systemUnderTest.GetSingleActor(id);


            /// Assert
            var actionResult = Assert.IsType<ActionResult<ServiceResponse<GetActorDto>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetSingleActor_ShouldReturn404Status()
        {
            /// Arrange
            int id = 15;
            var actorService = new Mock<IActorService>();
            actorService.Setup(x => x.GetSingleActor(id)).ReturnsAsync(ActorMockData.GetSingleActorIncorrect(id));
            var systemUnderTest = new ActorController(actorService.Object, new LoggerManager());

            /// Act
            var result = await systemUnderTest.GetSingleActor(id);


            /// Assert
            var actionResult = Assert.IsType<ActionResult<ServiceResponse<GetActorDto>>>(result);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task AddActorCorrect_ShouldReturn200Status()
        {
            /// Arrange
            var anyCorrectActor = new AddActorDto {FirstName = "Frank", LastName = "Lee", FilmIds = new List<int> { 1 } };
            var actorService = new Mock<IActorService>();
            actorService.Setup(x => x.AddActor(anyCorrectActor)).ReturnsAsync(ActorMockData.AddActorCorrect(anyCorrectActor));
            var systemUnderTest = new ActorController(actorService.Object, new LoggerManager());

            /// Act
            var result = await systemUnderTest.AddActor(anyCorrectActor);

            /// Assert
            var actionResult = Assert.IsType<ActionResult<ServiceResponse<List<GetActorDto>>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task AddActorIncorrect_ShouldReturn400Status()
        {
            /// Arrange           
            var actorService = new Mock<IActorService>();
            actorService.Setup(x => x.AddActor(null)).ReturnsAsync(ActorMockData.AddActorIncorrect(null));
            var systemUnderTest = new ActorController(actorService.Object, new LoggerManager());

            /// Act
            var result = await systemUnderTest.AddActor(null);

            /// Assert
            var actionResult = Assert.IsType<ActionResult<ServiceResponse<List<GetActorDto>>>>(result);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task UpdateActorCorrect_ShouldReturn200Status()
        {
            /// Arrange            
            var anyCorrectActor = new UpdateActorDto { FirstName = "Frank", LastName = "Lee", FilmIds = new List<int> { 1 } };
            var actorService = new Mock<IActorService>();
            actorService.Setup(x => x.UpdateActor(anyCorrectActor)).ReturnsAsync(ActorMockData.UpdateActorCorrect(anyCorrectActor));
            var systemUnderTest = new ActorController(actorService.Object, new LoggerManager());

            /// Act
            var result = await systemUnderTest.UpdateActor(anyCorrectActor);

            /// Assert
            var actionResult = Assert.IsType<ActionResult<ServiceResponse<GetActorDto>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task UpdateActorIncorrect_ShouldReturn400Status()
        {
            /// Arrange           
            var actorService = new Mock<IActorService>();
            actorService.Setup(x => x.UpdateActor(null)).ReturnsAsync(ActorMockData.UpdateActorIncorrect(null));
            var systemUnderTest = new ActorController(actorService.Object, new LoggerManager());

            /// Act
            var result = await systemUnderTest.UpdateActor(null);

            /// Assert
            var actionResult = Assert.IsType<ActionResult<ServiceResponse<GetActorDto>>>(result);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task DeleteActorCorrect_ShouldReturn200Status()
        {
            /// Arrange    
            int id = 3;
            var actorService = new Mock<IActorService>();
            actorService.Setup(x => x.DeleteActor(id)).ReturnsAsync(ActorMockData.DeleteActorCorrect(id));
            var systemUnderTest = new ActorController(actorService.Object, new LoggerManager());

            /// Act
            var result = await systemUnderTest.DeleteActor(id);

            /// Assert
            var actionResult = Assert.IsType<ActionResult<ServiceResponse<List<GetActorDto>>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task DeleteActorIncorrect_ShouldReturn404Status()
        {
            /// Arrange    
            int id = 15;
            var actorService = new Mock<IActorService>();
            actorService.Setup(x => x.DeleteActor(id)).ReturnsAsync(ActorMockData.DeleteActorIncorrect(id));
            var systemUnderTest = new ActorController(actorService.Object, new LoggerManager());

            /// Act
            var result = await systemUnderTest.DeleteActor(id);

            /// Assert
            var actionResult = Assert.IsType<ActionResult<ServiceResponse<List<GetActorDto>>>>(result);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        }
    }
}
