using AngularJsSample.Services.Messaging.Genres.Requests;
using AngularJsSample.Services.Messaging.Genres.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Services
{
    public interface IGenreService
    {
        AddMovieToGenreResponse AddMovieToGenre(AddMovieToGenreRequest request);
        DeleteGenreResponse DeleteGenre(DeleteGenreRequest request);
        DeleteMovieFromGenreResponse DeleteMovieFromGenre(DeleteMovieFromGenreRequest request);
        FindGenreMoviesResponse FindGenreMovies(FindGenreMoviesRequest request);
        GetAllGenresResponse GetAllGenres(GetAllGenresRequest request);
        GetGenreResponse GetGenre(GetGenreRequest request);
        SaveGenreResponse SaveGenre(SaveGenreRequest request);
    }
}
