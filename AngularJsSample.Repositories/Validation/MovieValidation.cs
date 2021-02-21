using AngularJsSample.Model.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AngularJsSample.Repositories.Validation
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

            if (item.MovieDescription == null || String.IsNullOrWhiteSpace(item.MovieName))
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
    }
}
