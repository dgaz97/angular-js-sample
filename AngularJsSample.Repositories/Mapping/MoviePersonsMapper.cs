using AngularJsSample.Model.MoviePersons;
using AngularJsSample.Repositories.DatabaseModel;
using System.Collections.Generic;
using System.Linq;

namespace AngularJsSample.Repositories.Mapping
{
    public static class MoviePersonsMapper
    {
        public static MoviePerson MapToModel(this MoviePerson_Get_Result dbResult)
        {
            if (dbResult == null) return null;
            return new MoviePerson()
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
                Biography=dbResult.Biography,
                ImageUrl = dbResult.ImageUrl,
                ImdbUrl = dbResult.ImdbUrl,
                Popularity = dbResult.Popularity
            };
        }

        public static List<MoviePerson> MapToModels (this IEnumerable<MoviePerson_GetAll_Result> dbResults)
        {
            var result = new List<MoviePerson>();
            if (dbResults == null) return result;

            result.AddRange(dbResults.Select(item => item.MapToModel()));
            return result;

        }

        public static MoviePerson MapToModel(this MoviePerson_GetAll_Result dbResult)
        {
            if (dbResult == null) return null;
            return new MoviePerson()
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
                Biography = dbResult.Biography,
                ImageUrl = dbResult.ImageUrl,
                ImdbUrl = dbResult.ImdbUrl,
                Popularity = dbResult.Popularity
            };
        }
    }
}
