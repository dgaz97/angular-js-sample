using AngularJsSample.Model.Movies;
using System.Collections.Generic;

namespace AngularJsSample.Model.Genres
{
    public interface IGenreRepository:IRepository<Genre, int>
    {
        List<Movie> FindMovies(int genreId);
        bool DeleteMovie(int genreId, int movieId, int userId);
        int AddMovie(int genreId, int movieId, int userId);
    }
}
