using System.Collections.Generic;
using AngularJsSample.Repositories.DatabaseModel;
using System.Linq;
using AngularJsSample.Model.MovieRatings;

namespace AngularJsSample.Repositories.Mapping
{
    public static class MovieRatingsMapper
    {
        public static MovieRating MapToModel(this MovieRating_GetByUserAndMovie_Result dbResult)
        {
            if (dbResult == null) return null;
            return new MovieRating()
            {
                DateCreated = dbResult.DateCreated.Value,
                UserCreated = new Model.Users.UserInfo()
                {
                    Id = dbResult.UserCreated,
                    FirstName = dbResult.UserCreatedFirstName,
                    LastName = dbResult.UserCreatedLastName,
                    FullName = dbResult.UserCreatedFullName,
                    Email = dbResult.UserCreatedEmail
                },
                Movie = new Model.Movies.Movie()
                {
                    MovieId = dbResult.MovieId,
                    MovieName = dbResult.MovieName,
                    MovieDescription = dbResult.MovieDescription,
                    MovieReleaseDate = dbResult.MovieReleaseDate,
                    MovieRating = dbResult.MovieRating,
                    MoviePosterUrl = dbResult.MoviePosterUrl,
                    MovieImdbUrl = dbResult.MovieImdbUrl
                },
                UserRating = dbResult.UserRating
            };
        }

        #region Mapper za MovieRating_GetAll_Result
        public static MovieRating MapToModel(this MovieRating_GetAll_Result dbResult)
        {
            if (dbResult == null) return null;
            return new MovieRating()
            {
                DateCreated = dbResult.DateCreated.Value,
                UserCreated = new Model.Users.UserInfo()
                {
                    Id = dbResult.UserCreated,
                    FirstName = dbResult.UserCreatedFirstName,
                    LastName = dbResult.UserCreatedLastName,
                    FullName = dbResult.UserCreatedFullName,
                    Email = dbResult.UserCreatedEmail
                },
                Movie = new Model.Movies.Movie()
                {
                    MovieId = dbResult.MovieId,
                    MovieName = dbResult.MovieName,
                    MovieDescription = dbResult.MovieDescription,
                    MovieReleaseDate = dbResult.MovieReleaseDate,
                    MovieRating = dbResult.MovieRating,
                    MoviePosterUrl = dbResult.MoviePosterUrl,
                    MovieImdbUrl = dbResult.MovieImdbUrl
                },
                UserRating = dbResult.UserRating
            };
        }

        public static List<MovieRating> MapToModels(this IEnumerable<MovieRating_GetAll_Result> dbResult)
        {
            var result = new List<MovieRating>();
            if (dbResult == null) return null;
            result.AddRange(dbResult.Select(item => item.MapToModel()));
            return result;
        }
        #endregion

        #region Mapper za MovieRating_GetByMovie_Result

        public static MovieRating MapToModel(this MovieRating_GetByMovie_Result dbResult)
        {
            if (dbResult == null) return null;
            return new MovieRating()
            {
                DateCreated = dbResult.DateCreated.Value,
                UserCreated = new Model.Users.UserInfo()
                {
                    Id = dbResult.UserCreated,
                    FirstName = dbResult.UserCreatedFirstName,
                    LastName = dbResult.UserCreatedLastName,
                    FullName = dbResult.UserCreatedFullName,
                    Email = dbResult.UserCreatedEmail
                },
                Movie = new Model.Movies.Movie()
                {
                    MovieId = dbResult.MovieId,
                    MovieName = dbResult.MovieName,
                    MovieDescription = dbResult.MovieDescription,
                    MovieReleaseDate = dbResult.MovieReleaseDate,
                    MovieRating = dbResult.MovieRating,
                    MoviePosterUrl = dbResult.MoviePosterUrl,
                    MovieImdbUrl = dbResult.MovieImdbUrl
                },
                UserRating = dbResult.UserRating
            };
        }
        public static List<MovieRating> MapToModels(this IEnumerable<MovieRating_GetByMovie_Result> dbResult)
        {
            var result = new List<MovieRating>();
            if (dbResult == null) return null;
            result.AddRange(dbResult.Select(item => item.MapToModel()));
            return result;
        }
        #endregion

        #region Mapper za MovieRating_GetByUser_Result
        public static MovieRating MapToModel(this MovieRating_GetByUser_Result dbResult)
        {
            if (dbResult == null) return null;
            return new MovieRating()
            {
                DateCreated = dbResult.DateCreated.Value,
                UserCreated = new Model.Users.UserInfo()
                {
                    Id = dbResult.UserCreated,
                    FirstName = dbResult.UserCreatedFirstName,
                    LastName = dbResult.UserCreatedLastName,
                    FullName = dbResult.UserCreatedFullName,
                    Email = dbResult.UserCreatedEmail
                },
                Movie = new Model.Movies.Movie()
                {
                    MovieId = dbResult.MovieId,
                    MovieName = dbResult.MovieName,
                    MovieDescription = dbResult.MovieDescription,
                    MovieReleaseDate = dbResult.MovieReleaseDate,
                    MovieRating = dbResult.MovieRating,
                    MoviePosterUrl = dbResult.MoviePosterUrl,
                    MovieImdbUrl = dbResult.MovieImdbUrl
                },
                UserRating = dbResult.UserRating
            };
        }
        public static List<MovieRating> MapToModels(this IEnumerable<MovieRating_GetByUser_Result> dbResult)
        {
            var result = new List<MovieRating>();
            if (dbResult == null) return null;
            result.AddRange(dbResult.Select(item => item.MapToModel()));
            return result;
        }
        #endregion
    }
}
