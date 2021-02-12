using AngularJsSample.Services.Messaging.Genres.Requests;
using AngularJsSample.Services.Messaging.Views.Movies;
using System.Collections.Generic;

namespace AngularJsSample.Services.Messaging.Genres.Responses
{
    public class FindGenreMoviesResponse:ResponseBase<FindGenreMoviesRequest>
    {
        public List<Movie> Movies { get; set; }
    }
}
