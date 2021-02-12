using AngularJsSample.Model.MovieRatings;
using AngularJsSample.Model.Movies;
using AngularJsSample.Repositories.DatabaseModel;
using AngularJsSample.Repositories.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public List<MovieRating> FindByMovie(Movie item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.MovieRating_GetByMovie(item.MovieId).MapToModels();
            }
        }

        public List<MovieRating> FindByUser(UserInfo item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.MovieRating_GetByUser(item.Id).MapToModels();
            }
        }

        public MovieRating FindByUserAndMovie(Movie item, UserInfo item2)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.MovieRating_GetByUserAndMovie(item2.Id, item.MovieId).SingleOrDefault().MapToModel();
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
