using AngularJsSample.Model.Genres;
using AngularJsSample.Model.Movies;
using AngularJsSample.Model.Users;
using AngularJsSample.Services.Messaging.Genres.Requests;
using System;

namespace AngularJsSample.Services.Validation
{
    /// <summary>
    /// Static class for validating Genre objects before insert/update
    /// </summary>
    public static class GenreValidation
    {
        /// <summary>
        /// Checks whether Genre object is valid, throws exception if not
        /// </summary>
        /// <param name="item">Genre object</param>
        public static void CheckGenreForInsertOrUpdate(this Genre item)
        {
            if (item.Name == null || String.IsNullOrWhiteSpace(item.Name)) throw new Exception("Genre name can't be empty");
            if (item.Name.Length > 50)
                throw new Exception("Genre name can't be greater than 50 characters");
            if (item.Description == null) item.Description = "";
            if (item.Description.Length > 1000)
                throw new Exception("Genre description can't be greater than 1000 characters");
        }

        public static void CheckIfGenreExists(this AddMovieToGenreRequest request, IGenreRepository repository)
        {
            var genre = repository.FindBy(request.GenreId);
            if(genre==null) throw new Exception($"Genre {request.GenreId} doesn't exist");
        }
        public static void CheckIfMovieExists(this AddMovieToGenreRequest request, IMovieRepository repository)
        {
            var movie = repository.FindBy(request.MovieId);
            if(movie==null) throw new Exception($"Movie {request.MovieId} doesn't exist");
        }
        public static void CheckIfGenreExists(this DeleteGenreRequest request, IGenreRepository repository)
        {
            var genre = repository.FindBy(request.GenreId);
            if(genre==null) throw new Exception($"Genre {request.GenreId} doesn't exist");
        }

        public static void CheckIfGenreExists(this DeleteMovieFromGenreRequest request, IGenreRepository repository)
        {
            var genre = repository.FindBy(request.GenreId);
            if (genre == null) throw new Exception($"Genre {request.GenreId} doesn't exist");
        }
        public static void CheckIfMovieExists(this DeleteMovieFromGenreRequest request, IMovieRepository repository)
        {
            var movie = repository.FindBy(request.MovieId);
            if (movie == null) throw new Exception($"Movie {request.MovieId} doesn't exist");
        }

        public static void CheckIfGenreExists(this SaveGenreRequest request, IGenreRepository repository)
        {
            var genre = repository.FindBy(request.Genre.GenreId);
            if (genre == null) throw new Exception($"Genre {request.Genre.GenreId} doesn't exist");
        }

    }
}
