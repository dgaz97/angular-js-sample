using AngularJsSample.Model.Movies;
using AngularJsSample.Repositories.DatabaseModel;
using System.Collections.Generic;
using System.Linq;

namespace AngularJsSample.Repositories.Mapping
{
    /// <summary>
    /// Static class for mapping movies between database results and model classes
    /// </summary>
    public static class MoviesMapper
    {
        /// <summary>
        /// Maps Movie from database to model class
        /// </summary>
        /// <param name="dbResult">Movie database result</param>
        /// <returns>Movie Model object</returns>
        public static Movie MapToModel (this Movie_Get_Result dbResult)
        {
            if (dbResult == null) return null;
            return new Movie()
            {
                MovieId = dbResult.MovieId,
                DateCreated = dbResult.MovieDateCreated,
                UserCreated = new Model.Users.UserInfo()
                {
                    Id = dbResult.UserCreated.Value,
                    FirstName = dbResult.UserCreatedFirstName,
                    LastName = dbResult.UserCreatedLastName,
                    FullName = dbResult.UserCreatedFullName,
                    Email = dbResult.UserCreatedEmail
                },
                DateLastModified = dbResult.MovieDateLastModified,
                UserLastModified = dbResult.UserLastModified.HasValue ? new Model.Users.UserInfo()
                {
                    Id = dbResult.UserLastModified.Value,
                    FirstName = dbResult.UserLastModifiedFirstName,
                    LastName = dbResult.UserLastModifiedLastName,
                    FullName = dbResult.UserLastModifiedFullName,
                    Email = dbResult.UserLastModifiedEmail
                } : null,
                MovieName = dbResult.MovieName,
                MovieDescription = dbResult.MovieDescription,
                MovieRating = dbResult.MovieRating,
                MovieImdbUrl = dbResult.MovieImdbUrl,
                MoviePosterUrl = dbResult.MoviePosterUrl,
                MovieReleaseDate = dbResult.MovieReleaseDate,
                Genres = null
            };
        }

        #region Mapper za Movie_GetAll_Result
        /// <summary>
        /// Maps Movie from database to model class
        /// </summary>
        /// <param name="dbResult">Movie database result</param>
        /// <returns>Movie Model object</returns>
        public static Movie MapToModel(this Movie_GetAll_Result dbResult)
        {
            if (dbResult == null) return null;
            return new Movie()
            {
                MovieId = dbResult.MovieId,
                DateCreated = dbResult.MovieDateCreated,
                UserCreated = new Model.Users.UserInfo()
                {
                    Id = dbResult.UserCreated.Value,
                    FirstName = dbResult.UserCreatedFirstName,
                    LastName = dbResult.UserCreatedLastName,
                    FullName = dbResult.UserCreatedFullName,
                    Email = dbResult.UserCreatedEmail
                },
                DateLastModified = dbResult.MovieDateLastModified,
                UserLastModified = dbResult.UserLastModified.HasValue ? new Model.Users.UserInfo()
                {
                    Id = dbResult.UserLastModified.Value,
                    FirstName = dbResult.UserLastModifiedFirstName,
                    LastName = dbResult.UserLastModifiedLastName,
                    FullName = dbResult.UserLastModifiedFullName,
                    Email = dbResult.UserLastModifiedEmail
                } : null,
                MovieName = dbResult.MovieName,
                MovieDescription = dbResult.MovieDescription,
                MovieRating = dbResult.MovieRating,
                MovieImdbUrl = dbResult.MovieImdbUrl,
                MoviePosterUrl = dbResult.MoviePosterUrl,
                MovieReleaseDate = dbResult.MovieReleaseDate,
                Genres = null
            };
        }
        /// <summary>
        /// Maps list of Movies from database to model class
        /// </summary>
        /// <param name="dbResult">IEnumerable of Movie database results</param>
        /// <returns>List of Movie models</returns>
        public static List<Movie> MapToModels(this IEnumerable<Movie_GetAll_Result> dbResult)
        {
            var result = new List<Movie>();
            if (dbResult == null) return result;
            result.AddRange(dbResult.Select(item => item.MapToModel()));
            return result;

        }
        #endregion

        #region Mapper za Genre_GetMovies_Result
        /// <summary>
        /// Maps Movie from database to model class
        /// </summary>
        /// <param name="dbResult">Movie database result</param>
        /// <returns>Movie Model object</returns>
        public static Movie MapToModel(this Genre_GetMovies_Result dbResult)
        {
            if (dbResult == null) return null;
            return new Movie()
            {
                MovieId = dbResult.MovieId,
                DateCreated = dbResult.MovieDateCreated,
                UserCreated = new Model.Users.UserInfo()
                {
                    Id = dbResult.UserCreated.Value,
                    FirstName = dbResult.UserCreatedFirstName,
                    LastName = dbResult.UserCreatedLastName,
                    FullName = dbResult.UserCreatedFullName,
                    Email = dbResult.UserCreatedEmail
                },
                DateLastModified = dbResult.MovieDateLastModified,
                UserLastModified = dbResult.UserLastModified.HasValue ? new Model.Users.UserInfo()
                {
                    Id = dbResult.UserLastModified.Value,
                    FirstName = dbResult.UserLastModifiedFirstName,
                    LastName = dbResult.UserLastModifiedLastName,
                    FullName = dbResult.UserLastModifiedFullName,
                    Email = dbResult.UserLastModifiedEmail
                } : null,
                MovieName = dbResult.MovieName,
                MovieDescription = dbResult.MovieDescription,
                MovieRating = dbResult.MovieRating,
                MovieImdbUrl = dbResult.MovieImdbUrl,
                MoviePosterUrl = dbResult.MoviePosterUrl,
                MovieReleaseDate = dbResult.MovieReleaseDate,
                Genres = null
            };
        }
        /// <summary>
        /// Maps list of Movies from database to model class
        /// </summary>
        /// <param name="dbResult">IEnumerable of Movie database results</param>
        /// <returns>List of Movie models</returns>
        public static List<Movie> MapToModels(this IEnumerable<Genre_GetMovies_Result> dbResult)
        {
            var result = new List<Movie>();
            if (dbResult == null) return result;
            result.AddRange(dbResult.Select(item => item.MapToModel()));
            return result;

        }
        #endregion
    }
}
