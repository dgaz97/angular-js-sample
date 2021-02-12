using AngularJsSample.Services.Messaging.Views.MoviePersons;

namespace AngularJsSample.Services.Messaging.MoviePersons
{
    public class SaveMoviePersonRequest:RequestBase
    {
        public MoviePerson MoviePerson { get; set; }
    }
}
