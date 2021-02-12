using AngularJsSample.Services.Messaging.MovieRatings.Requests;
using AngularJsSample.Services.Messaging.Views.MovieRatings;

namespace AngularJsSample.Services.Messaging.MovieRatings.Responses
{
    public class GetMovieRatingByMovieAndUserResponse:ResponseBase<GetMovieRatingByMovieAndUserRequest>
    {
        public MovieRating MovieRating { get; set; }
    }
}
