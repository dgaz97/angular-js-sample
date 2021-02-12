using AngularJsSample.Services.Messaging.Views.MovieRatings;

namespace AngularJsSample.Services.Messaging.MovieRatings.Requests
{
    public class SaveMovieRatingRequest:RequestBase
    {
        public MovieRating MovieRating { get; set;}
    }
}
