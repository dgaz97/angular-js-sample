using AngularJsSample.Model.MoviePersons;
using AngularJsSample.Repositories.DatabaseModel;
using System.Collections.Generic;
using System.Linq;

namespace AngularJsSample.Repositories.Mapping
{
    /// <summary>
    /// Static class for mapping movie persons between database results and model classes
    /// </summary>
    public static class MoviePersonsMapper
    {
        /// <summary>
        /// Maps movie person from database to model class
        /// </summary>
        /// <param name="dbResult">Movie person database result</param>
        /// <returns>Movie person Model object</returns>
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

        /// <summary>
        /// Maps list of Movie persons from database to model class
        /// </summary>
        /// <param name="dbResults">IEnumerable of Movie person database results</param>
        /// <returns>List of Movie person models</returns>
        public static List<MoviePerson> MapToModels (this IEnumerable<MoviePerson_GetAll_Result> dbResults)
        {
            var result = new List<MoviePerson>();
            if (dbResults == null) return result;

            result.AddRange(dbResults.Select(item => item.MapToModel()));
            return result;

        }

        /// <summary>
        /// Maps movie person from database to model class
        /// </summary>
        /// <param name="dbResult">Movie person database result</param>
        /// <returns>Movie person Model object</returns>
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
