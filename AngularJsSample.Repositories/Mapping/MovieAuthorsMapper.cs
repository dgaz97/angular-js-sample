using AngularJsSample.Model.MovieAuthors;
using AngularJsSample.Repositories.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Repositories.Mapping
{
    public static class MovieAuthorsMapper
    {
        public static MovieAuthor MapToModel(this MovieAuthor_Get_Result dbResult)
        {
            if (dbResult == null) return null;
            return new MovieAuthor()
            {
                Id = dbResult.Id,
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
                    FirstName=dbResult.UserLastModifiedFirstName,
                    LastName = dbResult.UserLastModifiedLastName,
                    FullName = dbResult.UserLastModifiedFullName,
                    Email = dbResult.UserLastModifiedEmail
                } : null,
                FirstName = dbResult.FirstName,
                LastName = dbResult.LastName,
                BirthDate =dbResult.BirthDate,
                BirthPlace=dbResult.BirthPlace,
                Biography=dbResult.BirthPlace,
                ImageUrl = dbResult.ImageUrl,
                ImdbUrl = dbResult.ImdbUrl
            };
        }

        public static List<MovieAuthor> MapToModels (this IEnumerable<MovieAuthor_GetAll_Result> dbResults)
        {
            var result = new List<MovieAuthor>();
            if (dbResults == null) return result;

            result.AddRange(dbResults.Select(item => item.MapToModel()));
            return result;

        }

        public static MovieAuthor MapToModel(this MovieAuthor_GetAll_Result dbResult)
        {
            if (dbResult == null) return null;
            return new MovieAuthor()
            {
                Id = dbResult.Id,
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
                FirstName = dbResult.FirstName,
                LastName = dbResult.LastName,
                BirthDate = dbResult.BirthDate,
                BirthPlace = dbResult.BirthPlace,
                Biography = dbResult.BirthPlace,
                ImageUrl = dbResult.ImageUrl,
                ImdbUrl = dbResult.ImdbUrl
            };
        }
    }
}
