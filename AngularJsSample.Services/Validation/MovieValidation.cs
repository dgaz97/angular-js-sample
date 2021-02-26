using AngularJsSample.Model.Genres;
using AngularJsSample.Model.MoviePersons;
using AngularJsSample.Model.MovieRoles;
using AngularJsSample.Model.Movies;
using AngularJsSample.Services.Messaging.Movies.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AngularJsSample.Services.Validation
{
    /// <summary>
    /// Static class for validating Movie objects before insert/update
    /// </summary>
    public static class MovieValidation
    {
        /// <summary>
        /// Checks whether Movie object is valid, throws exception if not
        /// </summary>
        /// <param name="item">Movie object</param>
        public static void CheckMovieForInsertOrUpdate(this Movie item)
        {

            if (item.MovieName == null || String.IsNullOrWhiteSpace(item.MovieName))
                throw new Exception("Movie name can't be empty");
            if (item.MovieName.Length > 200)
                throw new Exception("Movie name can't be greater than 200 characters");

            if (item.MovieDescription == null || String.IsNullOrWhiteSpace(item.MovieDescription))
                item.MovieDescription = "";
            if (item.MovieDescription.Length > 2000)
                throw new Exception("Movie description can't be greater than 2000 characters");

            if (item.MovieReleaseDate == DateTime.MinValue || item.MovieReleaseDate == null)
                throw new Exception("Movie release date can't be empty");

            //Movie rating is calculated in database after every new rating and is not written here

            Regex rxHttp = new Regex(@"^https?:\/\/");
            Regex rxImage = new Regex(@"\.jpg$|\.jpeg$|\.png$|\.gif$");
            if (!String.IsNullOrWhiteSpace(item.MoviePosterUrl))
            {
                if (item.MoviePosterUrl.Length > 200)
                    throw new Exception("Movie poster url must be under 200 characters, or empty");
                if (!rxHttp.IsMatch(item.MoviePosterUrl) || !rxImage.IsMatch(item.MoviePosterUrl)) throw new Exception("Poster url is invalid");
            }

            Regex rxImdb = new Regex(@"^https?:\/\/(www\.)?imdb.com/");
            if (String.IsNullOrWhiteSpace(item.MovieImdbUrl))
                throw new Exception("IMDb url can't be empty");
            if (item.MovieImdbUrl.Length > 100)
                throw new Exception("IMDb url can't be greater than 100 characters");
            if (!rxImdb.IsMatch(item.MovieImdbUrl)) throw new Exception("IMDb url is invalid");
        }

        public static void CheckIfMovieExists(this AddMoviePersonToMovieRequest request, IMovieRepository repository)
        {
            var movie = repository.FindBy(request.MovieId);
            if (movie == null) throw new Exception($"Movie {request.MovieId} doesn't exist");
        }
        public static void CheckIfMoviePersonExists(this AddMoviePersonToMovieRequest request, IMoviePersonRepository repository)
        {
            var moviePerson = repository.FindBy(request.MoviePersonId);
            if (moviePerson == null) throw new Exception($"Movie person {request.MoviePersonId} doesn't exist");
        }

        public static void CheckIfMovieRoleExists(this AddMoviePersonToMovieRequest request, IMovieRoleRepository repository)
        {
            var role = repository.FindBy(request.MovieRoleId);
            if (role == null) throw new Exception($"Movie role {request.MovieRoleId} doesn't exist");
        }


        public static void CheckIfMovieExists(this DeleteMovieRoleFromMovieRequest request, IMovieRepository repository)
        {
            var movie = repository.FindBy(request.MovieId);
            if (movie == null) throw new Exception($"Movie {request.MovieId} doesn't exist");
        }
        public static void CheckIfMoviePersonExists(this DeleteMovieRoleFromMovieRequest request, IMoviePersonRepository repository)
        {
            var moviePerson = repository.FindBy(request.MoviePersonId);
            if (moviePerson == null) throw new Exception($"Movie person {request.MoviePersonId} doesn't exist");
        }

        public static void CheckIfMovieRoleExists(this DeleteMovieRoleFromMovieRequest request, IMovieRoleRepository repository)
        {
            var role = repository.FindBy(request.MovieRoleId);
            if (role == null) throw new Exception($"Movie role {request.MovieRoleId} doesn't exist");
        }

        public static void CheckIfMovieExists(this FindMovieRolesRequest request, IMovieRepository repository)
        {
            var movie = repository.FindBy(request.MovieId);
            if (movie == null) throw new Exception($"Movie {request.MovieId} doesn't exist");
        }

        public static void CheckIfMovieExists(this AddGenreToMovieRequest request, IMovieRepository repository)
        {
            var movie = repository.FindBy(request.MovieId);
            if (movie == null) throw new Exception($"Movie {request.MovieId} doesn't exist");
        }
        public static void CheckIfGenreExists(this AddGenreToMovieRequest request, IGenreRepository repository)
        {
            var genre = repository.FindBy(request.GenreId);
            if (genre == null) throw new Exception($"Genre {request.GenreId} doesn't exist");
        }

        public static void CheckIfMovieExists(this DeleteGenreFromMovieRequest request, IMovieRepository repository)
        {
            var movie = repository.FindBy(request.MovieId);
            if (movie == null) throw new Exception($"Movie {request.MovieId} doesn't exist");
        }
        public static void CheckIfGenreExists(this DeleteGenreFromMovieRequest request, IGenreRepository repository)
        {
            var genre = repository.FindBy(request.GenreId);
            if (genre == null) throw new Exception($"Genre {request.GenreId} doesn't exist");
        }

        public static void CheckIfMovieExists(this DeleteMovieRequest request, IMovieRepository repository)
        {
            var movie = repository.FindBy(request.MovieId);
            if (movie == null) throw new Exception($"Movie {request.MovieId} doesn't exist");
        }
        public static void CheckIfMovieExists(this FindMovieGenresRequest request, IMovieRepository repository)
        {
            var movie = repository.FindBy(request.MovieId);
            if (movie == null) throw new Exception($"Movie {request.MovieId} doesn't exist");
        }

        public static void CheckIfMovieExists(this SaveMovieRequest request, IMovieRepository repository)
        {
            var movie = repository.FindBy(request.Movie.MovieId);
            if (movie == null) throw new Exception($"Movie {request.Movie.MovieId} doesn't exist");
        }


    }
}
