using System.Collections.Generic;
using System.Linq;
using AngularJsSample.Model.Genres;
using AngularJsSample.Model.Movies;
using AngularJsSample.Repositories.DatabaseModel;
using AngularJsSample.Repositories.Mapping;

namespace AngularJsSample.Repositories.Genres
{
    /// <summary>
    /// Repository for genres
    /// </summary>
    public class GenreRepository : IGenreRepository
    {
        /// <summary>
        /// Adds new genre
        /// </summary>
        /// <param name="item">Genre object</param>
        /// <returns>ID of new genre</returns>
        public int Add(Genre item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.Genre_Insert(item.UserCreated.Id, item.Name, item.Description);
            }
        }

        /// <summary>
        /// Deletes genre
        /// </summary>
        /// <param name="item">Genre object</param>
        /// <returns>true</returns>
        public bool Delete(Genre item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                context.Genre_Delete(item.GenreId, item.UserLastModified.Id);
                return true;
            }
        }

        /// <summary>
        /// Gets all genres
        /// </summary>
        /// <returns>List of genres</returns>
        public List<Genre> FindAll()
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.Genre_GetAll().MapToModels();
            }
        }

        /// <summary>
        /// Gets one genre
        /// </summary>
        /// <param name="key">ID of genre</param>
        /// <returns>Wanted Genre object</returns>
        public Genre FindBy(int key)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.Genre_Get(key).SingleOrDefault().MapToModel();
            }
        }

        /// <summary>
        /// Updates genre object
        /// </summary>
        /// <param name="item">Genre object with needed ID and data</param>
        /// <returns>Genre object</returns>
        public Genre Save(Genre item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                context.Genre_Update(item.GenreId, item.UserLastModified.Id, item.Name, item.Description);
                return FindBy(item.GenreId);
            }
        }

        /// <summary>
        /// Finds movies that have the requested genre
        /// </summary>
        /// <param name="genreId">Genre ID</param>
        /// <returns>List of movies that have the requested genre</returns>
        public List<Movie> FindMovies(int genreId)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.Genre_GetMovies(genreId).MapToModels();
            }
        }

        /// <summary>
        /// Deletes genre from movie
        /// </summary>
        /// <param name="genreId">Genre ID</param>
        /// <param name="movieId">Movie ID</param>
        /// <param name="userId">User ID that is deleting genre</param>
        /// <returns>true or false</returns>
        public bool DeleteMovie(int genreId, int movieId, int userId)//TODO:Might break? Gotta check this later
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                if(context.Genre_DeleteMovie(movieId, genreId, userId)==1)return true;
                else return false;
            }
        }

        /// <summary>
        /// Adds genre to movie
        /// </summary>
        /// <param name="genreId">Genre ID</param>
        /// <param name="movieId">Movie ID</param>
        /// <param name="userId">User ID that is adding genre</param>
        public int AddMovie (int genreId, int movieId, int userId)//TODO: might need to return null?
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.Genre_AddMovie(movieId, genreId, userId);
            }
        }

    }
}
