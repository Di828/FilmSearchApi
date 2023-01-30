using FilmSearch.Data;
using FilmSearch.Dtos.ActorD;
using FilmSearch.Models;
using FilmSearch.Services.ActorService;
using FilmSearch.Test.MockData;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmSearch.Test.Systems.Services
{
    public class TestActorService : IDisposable
    {
        protected readonly DataContext _context;        
        public TestActorService()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(options);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetAllActors_ReturnServiceResponseWithActorCollection()
        {
            /// Arrange   
            _context.AddRange(ActorMockData.Actors);
            _context.SaveChanges();
            var sut = new ActorService(_context);

            /// Act
            var result = await sut.GetAllActors();

            /// Assert
            Assert.True(result != null);
            Assert.True(result.Success);
            Assert.Equal(ActorMockData.Actors.Count, result.Data.Count);
        }

        [Fact]
        public async Task GetAllActorsWithEmptyDb_ReturnServiceResponseWithActorCollection()
        {
            /// Arrange               
            var sut = new ActorService(_context);

            /// Act
            var result = await sut.GetAllActors();

            /// Assert
            Assert.True(result != null);
            Assert.False(result.Success);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GetSingleActor_ReturnServiceResponseWithActor()
        {
            /// Arrange   
            int actorId = 1;
            int actorIndex = 0;
            _context.AddRange(ActorMockData.Actors);
            _context.SaveChanges();
            var sut = new ActorService(_context);

            /// Act
            var result = await sut.GetSingleActor(actorId);

            /// Assert
            Assert.True(result != null);
            Assert.True(result.Success);
            Assert.Equal(ActorMockData.Actors[actorIndex].FirstName, result.Data.FirstName);
        }

        [Fact]
        public async Task GetSingleActorEmptyDb_ReturnServiceResponseWithNoData()
        {
            /// Arrange   
            int actorId = 1;
            var sut = new ActorService(_context);

            /// Act
            var result = await sut.GetSingleActor(actorId);

            /// Assert
            Assert.True(result != null);
            Assert.False(result.Success);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GetSingleAbsentActor_ReturnServiceResponseWithNoData()
        {
            /// Arrange   
            int actorId = -1;            
            _context.AddRange(ActorMockData.Actors);
            _context.SaveChanges();
            var sut = new ActorService(_context);

            /// Act
            var result = await sut.GetSingleActor(actorId);

            /// Assert
            Assert.True(result != null);
            Assert.False(result.Success);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task AddActor_ReturnServiceResponseWithActorCollection()
        {
            /// Arrange   
            _context.AddRange(ActorMockData.Actors);            
            _context.SaveChanges();
            var sut = new ActorService(_context);

            /// Act
            var result = await sut.AddActor(new AddActorDto() {FirstName = "Another Actor", LastName = "LN", FilmIds = new List<int>() { 1 } });

            /// Assert
            Assert.True(result != null);
            Assert.True(result.Success);
            Assert.Equal(ActorMockData.Actors.Count + 1, result.Data.Count);
        }

        [Fact]
        public async Task AddActorWithoutName_ReturnServiceResponseWithNoData()
        {
            /// Arrange   
            _context.AddRange(ActorMockData.Actors);
            _context.SaveChanges();
            var sut = new ActorService(_context);

            /// Act
            var result = await sut.AddActor(new AddActorDto() { FirstName = "", LastName = "LN", FilmIds = new List<int>() { 1 } });

            /// Assert
            Assert.True(result != null);
            Assert.False(result.Success);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task AddActorWithoutLasName_ReturnServiceResponseWithNoData()
        {
            /// Arrange   
            _context.AddRange(ActorMockData.Actors);
            _context.SaveChanges();
            var sut = new ActorService(_context);

            /// Act
            var result = await sut.AddActor(new AddActorDto() { FirstName = "SomeName", LastName = "", FilmIds = new List<int>() { 1 } });

            /// Assert
            Assert.True(result != null);
            Assert.False(result.Success);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task AddActorWithIncorrectFilm_ReturnServiceResponseWithNoData()
        {
            /// Arrange   
            _context.AddRange(ActorMockData.Actors);
            _context.SaveChanges();
            var sut = new ActorService(_context);

            /// Act
            var result = await sut.AddActor(new AddActorDto() { FirstName = "SomeName", LastName = "LN", FilmIds = new List<int>() { -1 } });

            /// Assert
            Assert.True(result != null);
            Assert.False(result.Success);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task UpdateActor_ReturnServiceResponseWithGetActorDto()
        {
            /// Arrange   
            _context.AddRange(ActorMockData.Actors);
            _context.SaveChanges();
            var sut = new ActorService(_context);

            /// Act
            var result = await sut.UpdateActor(new UpdateActorDto() {Id = 1, FirstName = "Another Actor", LastName = "LN", FilmIds = new List<int>() { 1 } });

            /// Assert
            Assert.True(result != null);
            Assert.True(result.Success);
            Assert.Equal(ActorMockData.Actors[0].FirstName, result.Data.FirstName);
        }

        [Fact]
        public async Task UpdateActorIncorrectId_ReturnServiceResponseSucsessFalseDataNull()
        {
            /// Arrange   
            _context.AddRange(ActorMockData.Actors);
            _context.SaveChanges();
            var sut = new ActorService(_context);

            /// Act
            var result = await sut.UpdateActor(new UpdateActorDto() { Id = -1, FirstName = "Another Actor", LastName = "LN", FilmIds = new List<int>() { 1 } });

            /// Assert
            Assert.True(result != null);
            Assert.False(result.Success);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task DeleteActor_ReturnServiceResponseWithActorCollection()
        {
            /// Arrange   
            int actorId = 1;
            _context.AddRange(ActorMockData.Actors);
            _context.SaveChanges();
            var sut = new ActorService(_context);

            /// Act
            var result = await sut.DeleteActor(actorId);

            /// Assert
            Assert.True(result != null);
            Assert.True(result.Success);
            Assert.Equal(ActorMockData.Actors.Count - 1, result.Data.Count);
        }

        [Fact]
        public async Task DeleteActorIncorrectId_ReturnServiceResponseSucsessFalseDataNull()
        {
            /// Arrange   
            int actorId = -1;
            _context.AddRange(ActorMockData.Actors);
            _context.SaveChanges();
            var sut = new ActorService(_context);

            /// Act
            var result = await sut.DeleteActor(actorId);

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
