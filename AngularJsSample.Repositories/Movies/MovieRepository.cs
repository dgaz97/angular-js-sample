using AngularJsSample.Model.Genres;
using AngularJsSample.Model.Movies;
using AngularJsSample.Repositories.DatabaseModel;
using AngularJsSample.Repositories.Mapping;
using System.Collections.Generic;
using System.Linq;

namespace AngularJsSample.Repositories.Movies
{
    /// <summary>
    /// Repository for movies
    /// </summary>
    public class MovieRepository : IMovieRepository
    {
        /// <summary>
        /// Adds new movie
        /// </summary>
        /// <param name="item">Movie object</param>
        /// <returns>ID of newly created movie</returns>
        public int Add(Movie item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.Movie_Insert(item.UserCreated.Id, item.MovieName, item.MovieDescription, item.MovieReleaseDate, item.MoviePosterUrl, item.MovieImdbUrl).First().Value;
            }
        }
        /// <summary>
        /// Deletes movie
        /// </summary>
        /// <param name="item">Movie object</param>
        /// <returns>true</returns>
        public bool Delete(Movie item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                context.Movie_Delete(item.UserLastModified.Id, item.MovieId);
                return true;
            }
        }
        /// <summary>
        /// Gets a list of all movies
        /// </summary>
        /// <returns>List of movies</returns>
        public List<Movie> FindAll()
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.Movie_GetAll().MapToModels();
            }
        }

        /// <summary>
        /// Gets specific movie
        /// </summary>
        /// <param name="key">Movie ID</param>
        /// <returns>Movie object</returns>
        public Movie FindBy(int key)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.Movie_Get(key).SingleOrDefault().MapToModel();
            }
        }

        /// <summary>
        /// Updates movie
        /// </summary>
        /// <param name="item">Movie object with new data</param>
        /// <returns>Movie object</returns>
        public Movie Save(Movie item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                context.Movie_Update(item.UserLastModified.Id, item.MovieId, item.MovieName, item.MovieDescription, item.MovieReleaseDate, item.MoviePosterUrl, item.MovieImdbUrl);
                return FindBy(item.MovieId);
            }
        }
        /// <summary>
        /// Gets genres of movie
        /// </summary>
        /// <param name="key">ID of movie</param>
        /// <returns>List of genres</returns>
        public List<Genre> FindGenres(int key)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.Movie_GetGenres(key).MapToModels();
            }
        }

        /// <summary>
        /// Deletes genre from movie
        /// </summary>
        /// <param name="genreId">Genre ID</param>
        /// <param name="movieId">Movie ID</param>
        /// <param name="userId">ID of user that is deleting genre from movie</param>
        /// <returns>true</returns>
        public bool DeleteGenre(int genreId, int movieId, int userId)//TODO:Might break? Gotta check this later
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                context.Movie_DeleteGenre(movieId, genreId, userId);
                return true;
            }
        }
        /// <summary>
        /// Adds genre to movie
        /// </summary>
        /// <param name="genreId">Genre ID</param>
        /// <param name="movieId">Movie ID</param>
        /// <param name="userId">ID of user that is adding genre to movie</param>
        public int AddGenre (int genreId, int movieId, int userId)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.Movie_AddGenre(movieId, genreId, userId);
            }
        }
    }
}
