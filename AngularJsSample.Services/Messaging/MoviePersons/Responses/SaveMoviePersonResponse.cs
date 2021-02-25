using AngularJsSample.Services.Messaging.MoviePersons.Requests;
using AngularJsSample.Services.Messaging.Views.MoviePersons;

namespace AngularJsSample.Services.Messaging.MoviePersons.Responses
{
    public class SaveMoviePersonResponse:ResponseBase<SaveMoviePersonRequest>
    {
        public MoviePerson MoviePerson { get; set; }
    }
}
