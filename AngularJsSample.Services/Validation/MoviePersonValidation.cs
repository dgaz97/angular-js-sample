using AngularJsSample.Model.MoviePersons;
using AngularJsSample.Model.MovieRoles;
using AngularJsSample.Model.Movies;
using AngularJsSample.Services.Messaging;
using AngularJsSample.Services.Messaging.MoviePersons.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AngularJsSample.Services.Validation
{
    /// <summary>
    /// Static class for validating Movie person objects before insert/update
    /// </summary>
    public static class MoviePersonValidation
    {
        /// <summary>
        /// Checks whether Movie person objects is valid, throws exception if not
        /// </summary>
        /// <param name="item">Movie person object</param>
        public static void CheckDataForInsertOrUpdate(this MoviePerson item)
        {
            if (item.FirstName == null || String.IsNullOrWhiteSpace(item.FirstName)) throw new Exception("First name can't be empty");
            if (item.LastName == null || String.IsNullOrWhiteSpace(item.LastName)) throw new Exception("Last name can't be empty");
            if (item.BirthDate == DateTime.MinValue || item.BirthDate == null) throw new Exception("Birth date can't be empty");
            if (item.BirthPlace == null || String.IsNullOrWhiteSpace(item.BirthPlace)) throw new Exception("Birth place can't be empty");

            if (item.ImdbUrl == null || String.IsNullOrWhiteSpace(item.ImdbUrl)) throw new Exception("IMDb url can't be empty");
            Regex rxImdb = new Regex(@"^https?:\/\/(www\.)?imdb.com/");
            if (!rxImdb.IsMatch(item.ImdbUrl)) throw new Exception("IMDb url is invalid");

            if (!String.IsNullOrWhiteSpace(item.ImageUrl))
            {
                Regex rxHttp = new Regex(@"^https?:\/\/");
                Regex rxImage = new Regex(@"\.jpg$|\.jpeg$|\.png$|\.gif$");
                if (!rxHttp.IsMatch(item.ImageUrl) || !rxImage.IsMatch(item.ImageUrl)) throw new Exception("Image url is invalid");
            }
            //TODO: description must be under 2000 chars, firstname under 50, lastname under 50, birthplace under 50, imdburl under 100, imageurl under 200

            if (item.Popularity == null || item.Popularity <= 0) throw new Exception("Popularity can't be empty, or less than 1");
        }

        public static void CheckIfMoviePersonExists(this AddMovieToMoviePersonRequest request, IMoviePersonRepository repository)
        {
            var moviePerson = repository.FindBy(request.MoviePersonId);
            if (moviePerson == null) throw new Exception($"Movie person {request.MoviePersonId} does not exist");
        }
        public static void CheckIfMovieExists(this AddMovieToMoviePersonRequest request, IMovieRepository repository)
        {
            var movie = repository.FindBy(request.MovieId);
            if (movie == null) throw new Exception($"Movie {request.MovieId} does not exist");
        }
        public static void CheckIfMovieRoleExists(this AddMovieToMoviePersonRequest request, IMovieRoleRepository repository)
        {
            var role = repository.FindBy(request.MovieRoleId);
            if (role == null) throw new Exception($"Movie role {request.MovieRoleId} does not exist");
        }
        public static void CheckIfMoviePersonExists(this DeleteMovieFromMoviePersonRequest request, IMoviePersonRepository repository)
        {
            var moviePerson = repository.FindBy(request.MoviePersonId);
            if (moviePerson == null) throw new Exception($"Movie person {request.MoviePersonId} does not exist");
        }
        public static void CheckIfMovieExists(this DeleteMovieFromMoviePersonRequest request, IMovieRepository repository)
        {
            var movie = repository.FindBy(request.MovieId);
            if (movie == null) throw new Exception($"Movie {request.MovieId} does not exist");
        }
        public static void CheckIfMovieRoleExists(this DeleteMovieFromMoviePersonRequest request, IMovieRoleRepository repository)
        {
            var role = repository.FindBy(request.MovieRoleId);
            if (role == null) throw new Exception($"Movie role {request.MovieRoleId} does not exist");
        }
        public static void CheckIfMoviePersonExists(this DeleteMoviePersonRequest request, IMoviePersonRepository repository)
        {
            var person = repository.FindBy(request.MoviePersonId);
            if (person == null) throw new Exception($"Movie person {request.MoviePersonId} doesn't exist");
        }
        public static void CheckIfMoviePersonExists(this SaveMoviePersonRequest request, IMoviePersonRepository repository)
        {
            var person = repository.FindBy(request.MoviePerson.Id);
            if (person == null) throw new Exception($"Movie person {request.MoviePerson.Id} doesn't exist");
        }
    }
}
