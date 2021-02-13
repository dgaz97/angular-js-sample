namespace AngularJsSample.Services.Messaging.MovieRatings.Requests
{
    public class GetMovieRatingByMovieAndUserRequest:RequestBase
    {
        public int RequestedUser { get; set; }
        public int MovieId { get; set; }
    }
}
