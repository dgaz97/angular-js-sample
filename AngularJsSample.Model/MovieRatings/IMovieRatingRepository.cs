using System.Collections.Generic;

namespace AngularJsSample.Model.MovieRatings
{
    /// <summary>
    /// Interface for Movie Rating Repository
    /// </summary>
    public interface IMovieRatingRepository:IRepository<MovieRating, int>
    {
        /// <summary>
        /// Finds all ratings of movie
        /// </summary>
        /// <param name="movieId">Movie ID</param>
        /// <returns>List of movie ratings for movie</returns>
        List<MovieRating> FindByMovie(int movieId);
        /// <summary>
        /// Finds all ratings by user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>List of movie ratings made by user</returns>
        List<MovieRating> FindByUser(int userId);
        /// <summary>
        /// Finds the movie rating that the user made for movie
        /// </summary>
        /// <param name="movieId">Movie ID</param>
        /// <param name="userId">ID of user that made the rating</param>
        /// <returns>Movie rating of movie by user</returns>
        MovieRating FindByUserAndMovie(int movieId, int userId);
    }
}
