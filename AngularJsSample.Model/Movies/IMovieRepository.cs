using AngularJsSample.Model.Genres;
using System.Collections.Generic;

namespace AngularJsSample.Model.Movies
{
    /// <summary>
    /// Interface for Movie Repository
    /// </summary>
    public interface IMovieRepository:IRepository<Movie, int>
    {
        /// <summary>
        /// Gets a list of genres of movie
        /// </summary>
        /// <param name="key">Movie ID</param>
        /// <returns>List of genres of movie</returns>
        List<Genre> FindGenres(int key);
        /// <summary>
        /// Deletes genre from movie
        /// </summary>
        /// <param name="genreId">ID of genre</param>
        /// <param name="movieId">ID of movie</param>
        /// <param name="userId">ID of user that is deleting the genre from movie</param>
        bool DeleteGenre(int genreId, int movieId, int userId);
        /// <summary>
        /// Adds genre to movie
        /// </summary>
        /// <param name="genreId">ID of genre</param>
        /// <param name="movieId">ID of movie</param>
        /// <param name="userId">ID of user that is adding the genre to movie</param>
        int AddGenre(int genreId, int movieId, int userId);
        /// <summary>
        /// Gets genres of movie, cut down to optimize response size
        /// </summary>
        /// <param name="key">ID of movie</param>
        /// <returns>A light list of genres</returns>
        List<Genre> FindGenresLight(int key);
    }
}
