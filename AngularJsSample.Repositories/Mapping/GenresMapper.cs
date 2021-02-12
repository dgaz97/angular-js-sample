using AngularJsSample.Model.Genres;
using AngularJsSample.Repositories.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Repositories.Mapping
{
    public static class GenresMapper
    {
        public static Genre MapToModel (this Genre_Get_Result dbResult)
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
        public static Genre MapToModel (this Genre_GetAll_Result dbResult)
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
        

        public static List<Genre> MapToModels (this IEnumerable<Genre_GetAll_Result> dbResult)
        {
            var result = new List<Genre>();
            if (dbResult == null) return result;
            result.AddRange(dbResult.Select(item => item.MapToModel()));
            return result;
        }
        #endregion

        #region Mapper za Movie_GetGenres_Result

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

        public static List<Genre> MapToModels(this IEnumerable<Movie_GetGenres_Result> dbResult)
        {
            var result = new List<Genre>();
            if (dbResult == null) return result;
            result.AddRange(dbResult.Select(item => item.MapToModel()));
            return result;
        }
        #endregion
    }
}
