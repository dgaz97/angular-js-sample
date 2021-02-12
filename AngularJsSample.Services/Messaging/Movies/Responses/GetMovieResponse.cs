using AngularJsSample.Services.Messaging.Movies.Requests;
using AngularJsSample.Services.Messaging.Views.Movies;

namespace AngularJsSample.Services.Messaging.Movies.Responses
{
    public class GetMovieResponse:ResponseBase<GetMovieRequest>
    {
        public Movie Movie { get; set; }
    }
}
