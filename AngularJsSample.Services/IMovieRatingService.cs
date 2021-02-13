using AngularJsSample.Services.Messaging.MovieRatings.Requests;
using AngularJsSample.Services.Messaging.MovieRatings.Responses;

namespace AngularJsSample.Services
{
    public interface IMovieRatingService
    {
        GetAllMovieRatingsResponse GetAllMovieRatings(GetAllMovieRatingsRequest request);
        GetMovieRatingsByMovieResponse GetMovieRatingsByMovie(GetMovieRatingsByMovieRequest request);
        GetMovieRatingsByUserResponse GetMovieRatingsByUser(GetMovieRatingsByUserRequest request);
        GetMovieRatingByMovieAndUserResponse GetMovieRatingByMovieAndUser(GetMovieRatingByMovieAndUserRequest request);
        SaveMovieRatingResponse SaveMovieRating(SaveMovieRatingRequest request);
    }
}
