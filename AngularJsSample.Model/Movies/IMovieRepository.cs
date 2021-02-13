using AngularJsSample.Model.Genres;
using System.Collections.Generic;

namespace AngularJsSample.Model.Movies
{
    public interface IMovieRepository:IRepository<Movie, int>
    {
        List<Genre> FindGenres(int key);
        bool DeleteGenre(int genreId, int movieId, int userId);
        int AddGenre(int genreId, int movieId, int userId);
    }
}
