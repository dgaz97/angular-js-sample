using AngularJsSample.Services.Messaging.MoviePersons;

namespace AngularJsSample.Services
{
    public interface IMoviePersonService
    {
        GetAllMoviePersonsResponse GetAllMoviePersons(GetAllMoviePersonsRequest request);
        GetMoviePersonResponse GetMoviePerson(GetMoviePersonRequest request);
        SaveMoviePersonResponse SaveMoviePerson(SaveMoviePersonRequest request);
        DeleteMoviePersonResponse DeleteMoviePerson(DeleteMoviePersonRequest request);
    }
}
