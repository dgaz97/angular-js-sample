using AngularJsSample.Services.Messaging.MoviePersons.Requests;
using AngularJsSample.Services.Messaging.Views.MoviePersons;
using System.Collections.Generic;

namespace AngularJsSample.Services.Messaging.MoviePersons.Responses
{
    public class GetAllMoviePersonsResponse:ResponseBase<GetAllMoviePersonsRequest>
    {
        public List<MoviePerson> MoviePersons { get; set; }
    }
}
