namespace AngularJsSample.Services.Messaging.Movies.Requests
{
    public class DeleteGenreFromMovieRequest: RequestBase
    {
        public int MovieId { get; set; }
        public int GenreId { get; set; }
    }
}
