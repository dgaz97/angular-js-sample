namespace AngularJsSample.Services.Messaging.Genres.Requests
{
    public class DeleteMovieFromGenreRequest:RequestBase
    {
        public int GenreId { get; set; }
        public int MovieId { get; set; }
    }
}
