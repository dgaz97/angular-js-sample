using AngularJsSample.Model.Movies;
using AngularJsSample.Model.Users;
using AngularJsSample.Services.Messaging.MovieRatings.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Services.Validation
{
    public static class MovieRatingValidation
    {
        public static void CheckIfUserExists(this GetMovieRatingByMovieAndUserRequest request, IUserRepository repository)
        {
            var user = repository.FindBy(request.RequestedUser);
            if(user==null) throw new Exception($"User {request.RequestedUser} doesn't exist");
        }
        public static void CheckIfMovieExists(this GetMovieRatingByMovieAndUserRequest request, IMovieRepository repository)
        {
            var movie = repository.FindBy(request.MovieId);
            if(movie==null) throw new Exception($"Movie {request.MovieId} doesn't exist");
        }
        public static void CheckIfMovieExists(this GetMovieRatingsByMovieRequest request, IMovieRepository repository)
        {
            var movie = repository.FindBy(request.MovieId);
            if (movie == null) throw new Exception($"Movie {request.MovieId} doesn't exist");
        }
        public static void CheckIfUserExists(this GetMovieRatingsByUserRequest request, IUserRepository repository)
        {
            var user = repository.FindBy(request.RequestedUser);
            if (user == null) throw new Exception($"User {request.RequestedUser} doesn't exist");
        }
        public static void CheckIfMovieExists(this SaveMovieRatingRequest request, IMovieRepository repository)
        {
            var movie = repository.FindBy(request.MovieRating.Movie.MovieId);
            if (movie == null) throw new Exception($"Movie {request.MovieRating.Movie.MovieId} doesn't exist");
        }

        public static void CheckMovieRatingForInsert(this SaveMovieRatingRequest request)
        {
            if (request.MovieRating.UserRating < 0 || request.MovieRating.UserRating > 5)//TODO ili možda 10
                throw new Exception("Movie rating must be between 0 and 5");
        }
    }
}
