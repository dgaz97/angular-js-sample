using System.Collections.Generic;

namespace AngularJsSample.Model.MovieRatings
{
    public interface IMovieRatingRepository:IRepository<MovieRating, int>
    {
        List<MovieRating> FindByMovie(int movieId);
        List<MovieRating> FindByUser(int userId);
        MovieRating FindByUserAndMovie(int movieId, int userId);
    }
}
