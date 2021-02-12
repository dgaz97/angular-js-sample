namespace AngularJsSample.Services.Messaging.Movies.Requests
{
    public class AddGenreToMovieRequest: RequestBase
    {
        public int MovieId { get; set; }
        public int GenreId { get; set; }
    }
}
