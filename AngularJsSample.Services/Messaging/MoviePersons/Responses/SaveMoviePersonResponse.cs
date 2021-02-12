using AngularJsSample.Services.Messaging.Views.MoviePersons;

namespace AngularJsSample.Services.Messaging.MoviePersons
{
    public class SaveMoviePersonResponse:ResponseBase<SaveMoviePersonRequest>
    {
        public MoviePerson MoviePerson { get; set; }
    }
}
