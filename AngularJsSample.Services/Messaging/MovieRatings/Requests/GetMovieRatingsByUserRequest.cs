namespace AngularJsSample.Services.Messaging.MovieRatings.Requests
{
    public class GetMovieRatingsByUserRequest:RequestBase
    {
        public int RequestedUser { get; set; }
    }
}
