using AngularJsSample.Services.Messaging.MoviePersons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
