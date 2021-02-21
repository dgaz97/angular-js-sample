using AngularJsSample.Model.MovieRatings;
using AngularJsSample.Repositories.DatabaseModel;
using AngularJsSample.Repositories.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AngularJsSample.Repositories.MovieRatings
{
    /// <summary>
    /// Repository for movie ratings
    /// </summary>
    public class MovieRatingRepository : IMovieRatingRepository
    {
        /// <summary>
        /// Adds movie rating
        /// </summary>
        /// <param name="item">Movie rating object</param>
        public int Add(MovieRating item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.MovieRating_Insert(item.UserCreated.Id, item.Movie.MovieId, item.UserRating);
            }
        }

        /// <summary>
        /// Gets all movie ratings
        /// </summary>
        /// <returns>List of movie ratings</returns>
        public List<MovieRating> FindAll()
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.MovieRating_GetAll().MapToModels();
            }
        }

        /// <summary>
        /// Gets all ratings of requested movie
        /// </summary>
        /// <param name="movieId">Movie ID</param>
        /// <returns>List of movie rating for movie</returns>
        public List<MovieRating> FindByMovie(int movieId)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.MovieRating_GetByMovie(movieId).MapToModels();
            }
        }
        /// <summary>
        /// Gets all ratings by user
        /// </summary>
        /// <param name="userId">ID of user</param>
        /// <returns>List of ratings made by user</returns>
        public List<MovieRating> FindByUser(int userId)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.MovieRating_GetByUser(userId).MapToModels();
            }
        }
        /// <summary>
        /// Gets rating of movie by user
        /// </summary>
        /// <param name="movieId">Movie ID</param>
        /// <param name="userId">User ID</param>
        /// <returns>Movie rating of movie made by user</returns>
        public MovieRating FindByUserAndMovie(int movieId, int userId)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.MovieRating_GetByUserAndMovie(userId, movieId).SingleOrDefault().MapToModel();
            }
        }

        #region NotImplementedException
        public MovieRating Save(MovieRating item)
        {
            throw new NotImplementedException();
        }
        public bool Delete(MovieRating item)//TODO: Možda sa Add, sa ratingom 0?
        {
            throw new NotImplementedException();
        }
        public MovieRating FindBy(int key)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
