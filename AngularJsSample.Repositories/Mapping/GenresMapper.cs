using AngularJsSample.Model.Genres;
using AngularJsSample.Repositories.DatabaseModel;
using System.Collections.Generic;
using System.Linq;

namespace AngularJsSample.Repositories.Mapping
{
    /// <summary>
    /// Static class for mapping genres between database results and model classes
    /// </summary>
    public static class GenresMapper
    {
        /// <summary>
        /// Maps Genre from database to model class
        /// </summary>
        /// <param name="dbResult">Genre database result</param>
        /// <returns>Genre Model object</returns>
        public static Genre MapToModel(this Genre_Get_Result dbResult)
        {
            if (dbResult == null) return null;
            return new Genre()
            {
                GenreId = dbResult.GenreId,
                DateCreated = dbResult.DateCreated,
                UserCreated = new Model.Users.UserInfo()
                {
                    Id = dbResult.UserCreated.Value,
                    FirstName = dbResult.UserCreatedFirstName,
                    LastName = dbResult.UserCreatedLastName,
                    FullName = dbResult.UserCreatedFullName,
                    Email = dbResult.UserCreatedEmail
                },
                DateLastModified = dbResult.DateLastModified,
                UserLastModified = dbResult.UserLastModified.HasValue ? new Model.Users.UserInfo()
                {
                    Id = dbResult.UserLastModified.Value,
                    FirstName = dbResult.UserLastModifiedFirstName,
                    LastName = dbResult.UserLastModifiedLastName,
                    FullName = dbResult.UserLastModifiedFullName,
                    Email = dbResult.UserLastModifiedEmail
                } : null,
                Name = dbResult.Name,
                Description = dbResult.Description
            };
        }

        #region Mapper za Genre_GetAll_Result
        /// <summary>
        /// Maps Genre from database to model class
        /// </summary>
        /// <param name="dbResult">Genre database result</param>
        /// <returns>Genre Model object</returns>
        public static Genre MapToModel(this Genre_GetAll_Result dbResult)
        {
            if (dbResult == null) return null;
            return new Genre()
            {
                GenreId = dbResult.GenreId,
                DateCreated = dbResult.DateCreated,
                UserCreated = new Model.Users.UserInfo()
                {
                    Id = dbResult.UserCreated.Value,
                    FirstName = dbResult.UserCreatedFirstName,
                    LastName = dbResult.UserCreatedLastName,
                    FullName = dbResult.UserCreatedFullName,
                    Email = dbResult.UserCreatedEmail
                },
                DateLastModified = dbResult.DateLastModified,
                UserLastModified = dbResult.UserLastModified.HasValue ? new Model.Users.UserInfo()
                {
                    Id = dbResult.UserLastModified.Value,
                    FirstName = dbResult.UserLastModifiedFirstName,
                    LastName = dbResult.UserLastModifiedLastName,
                    FullName = dbResult.UserLastModifiedFullName,
                    Email = dbResult.UserLastModifiedEmail
                } : null,
                Name = dbResult.Name,
                Description = dbResult.Description
            };
        }
        /// <summary>
        /// Maps list of Genres from database to model class
        /// </summary>
        /// <param name="dbResult">IEnumerable of Genre database results</param>
        /// <returns>List of Genre models</returns>
        public static List<Genre> MapToModels(this IEnumerable<Genre_GetAll_Result> dbResult)
        {
            var result = new List<Genre>();
            if (dbResult == null) return result;
            result.AddRange(dbResult.Select(item => item.MapToModel()));
            return result;
        }
        #endregion

        #region Mapper za Movie_GetGenres_Result

        /// <summary>
        /// Maps Genre from database to model class
        /// </summary>
        /// <param name="dbResult">Genre database result</param>
        /// <returns>Genre Model object</returns>
        public static Genre MapToModel(this Movie_GetGenres_Result dbResult)
        {
            if (dbResult == null) return null;
            return new Genre()
            {
                GenreId = dbResult.GenreId,
                DateCreated = dbResult.DateCreated,
                UserCreated = new Model.Users.UserInfo()
                {
                    Id = dbResult.UserCreated.Value,
                    FirstName = dbResult.UserCreatedFirstName,
                    LastName = dbResult.UserCreatedLastName,
                    FullName = dbResult.UserCreatedFullName,
                    Email = dbResult.UserCreatedEmail
                },
                DateLastModified = dbResult.DateLastModified,
                UserLastModified = dbResult.UserLastModified.HasValue ? new Model.Users.UserInfo()
                {
                    Id = dbResult.UserLastModified.Value,
                    FirstName = dbResult.UserLastModifiedFirstName,
                    LastName = dbResult.UserLastModifiedLastName,
                    FullName = dbResult.UserLastModifiedFullName,
                    Email = dbResult.UserLastModifiedEmail
                } : null,
                Name = dbResult.Name,
                Description = dbResult.Description
            };
        }
        /// <summary>
        /// Maps list of Genres from database to model class
        /// </summary>
        /// <param name="dbResult">IEnumerable of Genre database results</param>
        /// <returns>List of Genre models</returns>
        public static List<Genre> MapToModels(this IEnumerable<Movie_GetGenres_Result> dbResult)
        {
            var result = new List<Genre>();
            if (dbResult == null) return result;
            result.AddRange(dbResult.Select(item => item.MapToModel()));
            return result;
        }
        #endregion

        #region Mapper za Movie_GetGenresLight_Result
        /// <summary>
        /// Maps Genre from database to model class
        /// </summary>
        /// <param name="dbResult">Genre database result</param>
        /// <returns>Genre Model object</returns>
        public static Genre MapToModel(this Movie_GetGenresLight_Result dbResult)
        {
            if (dbResult == null)
                return null;
            return new Genre()
            {
                GenreId = dbResult.GenreId,
                Name = dbResult.Name,
                UserCreated = new Model.Users.UserInfo()
                {
                    Id = dbResult.UserCreated.Value
                },
                DateCreated = dbResult.DateCreated,
                UserLastModified = dbResult.UserLastModified.HasValue ? new Model.Users.UserInfo()
                {
                    Id = dbResult.UserLastModified.Value
                } : null,
                DateLastModified = dbResult.DateLastModified,
            };
        }

        /// <summary>
        /// Maps list of Genres from database to model class
        /// </summary>
        /// <param name="dbresult">IEnumerable of Genre database results</param>
        /// <returns>List of Genre models</returns>
        public static List<Genre> MapToModels(this IEnumerable<Movie_GetGenresLight_Result> dbresult)
        {
            var result = new List<Genre>();
            if (dbresult == null) return result;
            result.AddRange(dbresult.Select(item => item.MapToModel()));
            return result;
        }
        #endregion
    }
}
