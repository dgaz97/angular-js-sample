using AngularJsSample.Services.Messaging.MoviePersons.Requests;
using AngularJsSample.Services.Messaging.MoviePersons.Responses;

namespace AngularJsSample.Services
{
    public interface IMoviePersonService
    {
        GetAllMoviePersonsResponse GetAllMoviePersons(GetAllMoviePersonsRequest request);
        GetMoviePersonResponse GetMoviePerson(GetMoviePersonRequest request);
        SaveMoviePersonResponse SaveMoviePerson(SaveMoviePersonRequest request);
        DeleteMoviePersonResponse DeleteMoviePerson(DeleteMoviePersonRequest request);
        AddMovieToMoviePersonResponse AddMovieToMoviePerson(AddMovieToMoviePersonRequest request);
        DeleteMovieFromMoviePersonResponse DeleteMovieFromMoviePerson(DeleteMovieFromMoviePersonRequest request);
        FindMoviePersonRolesResponse FindMoviePersonRoles(FindMoviePersonRolesRequest request);
    }
}
