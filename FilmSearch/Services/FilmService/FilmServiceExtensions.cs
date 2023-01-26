using Microsoft.EntityFrameworkCore;

namespace FilmSearch.Services.FilmServiceExtensions
{
    public static class FilmServiceExtensions
    {
        public static bool UpdateActors(this Film film)
        {
            if (film.Actors is not null)
            {
                foreach (var actor in film.Actors)
                {
                    if (string.IsNullOrEmpty(actor.FirstName) || string.IsNullOrEmpty(actor.LastName))
                    {
                        return false;
                    }

                    if (actor.Films is null)
                    {
                        actor.Films = new List<Film>();
                    }

                    actor.Films.Add(film);
                }
            }

            return true;
        }

        public static void UpdateReviews(this Film film)
        {
            if (film.Reviews is not null)
            {
                foreach (var review in film.Reviews)
                {
                    review.ReviewedFilm = film;
                }
            }
        }
    }
}
