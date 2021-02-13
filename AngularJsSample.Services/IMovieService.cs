using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngularJsSample.Services.Messaging.Movies.Requests;
using AngularJsSample.Services.Messaging.Movies.Responses;

namespace AngularJsSample.Services
{
    public interface IMovieService
    {
        AddGenreToMovieResponse AddGenreToMovie(AddGenreToMovieRequest request);
        DeleteGenreFromMovieResponse DeleteGenreFromMovie(DeleteGenreFromMovieRequest request);
        DeleteMovieResponse DeleteMovie(DeleteMovieRequest request);
        FindMovieGenresResponse FindMovieGenres(FindMovieGenresRequest request);
        GetAllMoviesResponse GetAllMovies(GetAllMoviesRequest request);
        GetMovieResponse GetMovie(GetMovieRequest request);
        SaveMovieResponse SaveMovie(SaveMovieRequest request);
    }
}
