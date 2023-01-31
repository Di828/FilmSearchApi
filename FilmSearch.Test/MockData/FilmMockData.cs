using FilmSearch.Dtos.ActorD;
using FilmSearch.Dtos.FilmD;
using FilmSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmSearch.Test.MockData
{
    public class FilmMockData
    {
        public static readonly List<Film> Films = new List<Film>()
        {            
                    new Film(){Id = 1, Title = "Titanic"},                            
                    new Film(){Id = 2, Title = "Hello"},                            
                    new Film(){Id = 3, Title = "NoNamed"}                
        };

        private static List<GetFilmDto> _filmsList = new List<GetFilmDto>
        {
            new GetFilmDto(){Id = 1, Title = "First Film"},
            new GetFilmDto(){Id = 2, Title = "Second Film"},
            new GetFilmDto(){Id = 3, Title = "Third Film"},
        };

        public static ServiceResponse<List<GetFilmDto>> GetAllFilms()
        {
            var response = new ServiceResponse<List<GetFilmDto>>();
            response.Data = _filmsList;

            return response;
        }

        public static ServiceResponse<GetFilmDto> GetSingleFilmCorrect(int id)
        {
            var response = new ServiceResponse<GetFilmDto>();
            response.Data = _filmsList[0];

            return response;
        }

        public static ServiceResponse<GetFilmDto> GetSingleFilmIncorrect(int id)
        {
            var response = new ServiceResponse<GetFilmDto>();
            response.Success = false;

            return response;
        }

        public static ServiceResponse<List<GetFilmDto>> AddFilmCorrect(AddFilmDto newFilm)
        {
            var response = new ServiceResponse<List<GetFilmDto>>();
            response.Data = _filmsList;

            return response;
        }

        public static ServiceResponse<List<GetFilmDto>> AddFilmIncorrect(AddFilmDto newFilm)
        {
            var response = new ServiceResponse<List<GetFilmDto>>();
            response.Success = false;

            return response;
        }

        public static ServiceResponse<List<GetFilmDto>> DeleteFilmCorrect(int id)
        {
            var response = new ServiceResponse<List<GetFilmDto>>();
            response.Data = _filmsList;

            return response;
        }

        public static ServiceResponse<List<GetFilmDto>> DeleteFilmIncorrect(int id)
        {
            var response = new ServiceResponse<List<GetFilmDto>>();
            response.Success = false;

            return response;
        }
    }
}
