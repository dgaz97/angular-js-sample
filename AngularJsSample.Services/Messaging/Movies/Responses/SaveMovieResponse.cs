using AngularJsSample.Services.Messaging.Movies.Requests;
using AngularJsSample.Services.Messaging.Views.Movies;

namespace AngularJsSample.Services.Messaging.Movies.Responses
{
    public class SaveMovieResponse:ResponseBase<SaveMovieRequest>
    {
        public Movie Movie { get; set; }
    }
}
