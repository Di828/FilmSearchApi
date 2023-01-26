using System.Reflection.PortableExecutable;

namespace FilmSearch
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Film, GetFilmDto>();
            CreateMap<AddFilmDto, Film>();
            CreateMap<ActorInFilmDto, Actor>();
            CreateMap<Actor, ActorInFilmDto>();            
            CreateMap<ReviewInFilmDto, Review>();
            CreateMap<Review, ReviewInFilmDto>();
        }
    }
}
