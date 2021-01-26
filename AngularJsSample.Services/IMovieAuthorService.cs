using AngularJsSample.Services.Messaging.MovieAuthors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Services
{
    public interface IMovieAuthorService
    {
        GetAllMovieAuthorsResponse GetAllMovieAuthors(GetAllMovieAuthorsRequest request);
        GetMovieAuthorResponse GetMovieAuthor(GetMovieAuthorRequest request);
        SaveMovieAuthorResponse SaveMovieAuthor(SaveMovieAuthorRequest request);
        DeleteMovieAuthorResponse DeleteMovieAuthor(DeleteMovieAuthorRequest request);
    }
}
