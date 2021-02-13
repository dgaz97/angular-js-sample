using AngularJsSample.Model.MovieRatings;
using AngularJsSample.Repositories.DatabaseModel;
using AngularJsSample.Repositories.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AngularJsSample.Repositories.MovieRatings
{
    public class MovieRatingRepository : IMovieRatingRepository
    {
        public int Add(MovieRating item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.MovieRating_Insert(item.UserCreated.Id, item.Movie.MovieId, item.UserRating);
            }
        }


        public List<MovieRating> FindAll()
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.MovieRating_GetAll().MapToModels();
            }
        }


        public List<MovieRating> FindByMovie(int movieId)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.MovieRating_GetByMovie(movieId).MapToModels();
            }
        }

        public List<MovieRating> FindByUser(int userId)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.MovieRating_GetByUser(userId).MapToModels();
            }
        }

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
