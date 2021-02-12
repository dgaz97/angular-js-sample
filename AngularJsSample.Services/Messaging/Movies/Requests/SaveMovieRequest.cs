using AngularJsSample.Services.Messaging.Views.Movies;

namespace AngularJsSample.Services.Messaging.Movies.Requests
{
    public class SaveMovieRequest: RequestBase
    {
        public Movie Movie { get; set; }
    }
}
