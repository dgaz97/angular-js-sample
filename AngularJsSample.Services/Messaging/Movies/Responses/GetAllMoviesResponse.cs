using AngularJsSample.Services.Messaging.Movies.Requests;
using AngularJsSample.Services.Messaging.Views.Movies;
using System.Collections.Generic;

namespace AngularJsSample.Services.Messaging.Movies.Responses
{
    public class GetAllMoviesResponse:ResponseBase<GetAllMoviesRequest>
    {
        public List<Movie> Movies { get; set; }
    }
}
