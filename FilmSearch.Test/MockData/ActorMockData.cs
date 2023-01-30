using Azure;
using FilmSearch.Dtos.ActorD;
using FilmSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmSearch.Test.MockData
{
    public class ActorMockData
    {
        public static readonly List<Actor> Actors = new List<Actor>()
        {
            new Actor {Id = 1, FirstName = "FirstActor", LastName = "LN",
                Films = new List<Film>
                {
                    new Film(){Id = 1, Title = "Titanic"}
                } },
            new Actor {Id = 2, FirstName = "SecondActor", LastName = "LN",
                Films = new List<Film>
                {
                    new Film(){Id = 2, Title = "Hello"}
                } },
            new Actor {Id = 3, FirstName = "ThirdActor", LastName = "LN",
                Films = new List<Film>
                {
                    new Film(){Id = 3, Title = "NoNamed"}
                } },
        };

        private static List<GetActorDto> actorsList = new List<GetActorDto>
        {
                new GetActorDto() { Id = 1, FirstName = "First Actor", LastName = "LN", Films = new List<string> {"First Film"} },
                new GetActorDto() { Id = 2, FirstName = "Second Actor", LastName = "LN", Films = new List<string> {"Second Film"} },
                new GetActorDto() { Id = 3, FirstName = "Third Actor", LastName = "LN", Films = new List<string> {"Third Film"} },
        };
        public static ServiceResponse<List<GetActorDto>> GetAllActorsDto()
        {
            var response = new ServiceResponse<List<GetActorDto>>();            
            response.Data = actorsList;

            return response;
        }

        public static ServiceResponse<List<GetActorDto>> GetAllActorsEmptyDto()
        {
            var response = new ServiceResponse<List<GetActorDto>>();
            response.Success = false;
            response.Data = null;

            return response;
        }

        public static ServiceResponse<GetActorDto> GetSingleActorCorrect(int id)
        {
            var response = new ServiceResponse<GetActorDto>();
            response.Data = actorsList[0];

            return response;
        }

        public static ServiceResponse<GetActorDto> GetSingleActorIncorrect(int id)
        {
            var response = new ServiceResponse<GetActorDto>();
            response.Success = false;

            return response;
        }

        public static ServiceResponse<List<GetActorDto>> AddActorCorrect(AddActorDto newActor)
        {
            var response = new ServiceResponse<List<GetActorDto>>();
            response.Data = actorsList;

            return response;
        }

        public static ServiceResponse<List<GetActorDto>> AddActorIncorrect(AddActorDto newActor)
        {
            var response = new ServiceResponse<List<GetActorDto>>();
            response.Success = false;
            response.Message = string.Empty;
            response.Data = null;

            return response;
        }
        public static ServiceResponse<GetActorDto> UpdateActorCorrect(UpdateActorDto newActor)
        {
            var response = new ServiceResponse<GetActorDto>();
            response.Data = actorsList[1];

            return response;
        }

        public static ServiceResponse<GetActorDto> UpdateActorIncorrect(UpdateActorDto newActor)
        {
            var response = new ServiceResponse<GetActorDto>();
            response.Success = false;
            response.Message = string.Empty;
            response.Data = null;

            return response;
        }

        public static ServiceResponse<List<GetActorDto>> DeleteActorCorrect(int id)
        {
            var response = new ServiceResponse<List<GetActorDto>>();
            response.Data = actorsList;

            return response;
        }

        public static ServiceResponse<List<GetActorDto>> DeleteActorIncorrect(int id)
        {
            var response = new ServiceResponse<List<GetActorDto>>();
            response.Success = false;

            return response;
        }

    }
}
