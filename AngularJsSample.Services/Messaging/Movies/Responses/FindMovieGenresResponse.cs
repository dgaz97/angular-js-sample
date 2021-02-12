using AngularJsSample.Services.Messaging.Movies.Requests;
using AngularJsSample.Services.Messaging.Views.Genres;
using System.Collections.Generic;

namespace AngularJsSample.Services.Messaging.Movies.Responses
{
    public class FindMovieGenresResponse:ResponseBase<FindMovieGenresRequest>
    {
        public List<Genre> Genres { get; set; }
    }
}
