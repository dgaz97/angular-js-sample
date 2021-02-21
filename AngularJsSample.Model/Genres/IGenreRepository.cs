using AngularJsSample.Model.Movies;
using System.Collections.Generic;

namespace AngularJsSample.Model.Genres
{
    /// <summary>
    /// Interface for Genre repository
    /// </summary>
    public interface IGenreRepository:IRepository<Genre, int>
    {
        /// <summary>
        /// Gets a list of movies that have the requested genre
        /// </summary>
        /// <param name="genreId">ID of genre</param>
        /// <returns>A list of movies that have the requested genre</returns>
        List<Movie> FindMovies(int genreId);
        /// <summary>
        /// Removes genre from movie
        /// </summary>
        /// <param name="genreId">Genre ID</param>
        /// <param name="movieId">Movie ID</param>
        /// <param name="userId">User that is deleting genre from movie</param>
        bool DeleteMovie(int genreId, int movieId, int userId);
        /// <summary>
        /// Adds genre to movie
        /// </summary>
        /// <param name="genreId">Genre ID</param>
        /// <param name="movieId">Movie ID</param>
        /// <param name="userId">User that is adding genre ti movie</param>
        int AddMovie(int genreId, int movieId, int userId);
    }
}
