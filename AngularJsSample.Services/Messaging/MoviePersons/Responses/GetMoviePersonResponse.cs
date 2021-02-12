using AngularJsSample.Services.Messaging.Views.MoviePersons;

namespace AngularJsSample.Services.Messaging.MoviePersons
{
    public class GetMoviePersonResponse : ResponseBase<GetMoviePersonRequest>
    {
        public MoviePerson MoviePerson { get; set; }
    }
}
