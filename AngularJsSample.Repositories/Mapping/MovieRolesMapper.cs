using System;
using System.Collections.Generic;
using AngularJsSample.Repositories.DatabaseModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngularJsSample.Model.MovieRoles;

namespace AngularJsSample.Repositories.Mapping
{
    public static class MovieRolesMapper
    {
        public static MovieRole MapToModel(this MovieRole_Get_Result dbResult)
        {
            if (dbResult == null) return null;
            return new MovieRole()
            {
                MovieRoleId = dbResult.MovieRoleId,
                MovieRoleName = dbResult.MovieRoleName,
                MovieRoleDescription = dbResult.MovieRoleDescription,
                DateCreated = dbResult.MovieRoleDateCreated,
                DateLastModified = dbResult.MovieRoleDateLastModified,
                UserCreated = new Model.Users.UserInfo()
                {
                    Id = dbResult.UserCreated.Value,
                    FirstName = dbResult.UserCreatedFirstName,
                    LastName = dbResult.UserCreatedLastName,
                    FullName = dbResult.UserCreatedFullName,
                    Email = dbResult.UserCreatedEmail
                },
                UserLastModified = dbResult.UserLastModified.HasValue ? new Model.Users.UserInfo()
                {
                    Id = dbResult.UserLastModified.Value,
                    FirstName = dbResult.UserLastModifiedFirstName,
                    LastName = dbResult.UserLastModifiedLastName,
                    FullName = dbResult.UserLastModifiedFullName,
                    Email = dbResult.UserLastModifiedEmail
                } : null
            };
        }

        public static MovieRole MapToModel(this MovieRole_GetAll_Result dbResult)
        {
            if (dbResult == null) return null;
            return new MovieRole()
            {
                MovieRoleId = dbResult.MovieRoleId,
                MovieRoleName = dbResult.MovieRoleName,
                MovieRoleDescription = dbResult.MovieRoleDescription,
                DateCreated = dbResult.MovieRoleDateCreated,
                DateLastModified = dbResult.MovieRoleDateLastModified,
                UserCreated = new Model.Users.UserInfo()
                {
                    Id = dbResult.UserCreated.Value,
                    FirstName = dbResult.UserCreatedFirstName,
                    LastName = dbResult.UserCreatedLastName,
                    FullName = dbResult.UserCreatedFullName,
                    Email = dbResult.UserCreatedEmail
                },
                UserLastModified = dbResult.UserLastModified.HasValue ? new Model.Users.UserInfo()
                {
                    Id = dbResult.UserLastModified.Value,
                    FirstName = dbResult.UserLastModifiedFirstName,
                    LastName = dbResult.UserLastModifiedLastName,
                    FullName = dbResult.UserLastModifiedFullName,
                    Email = dbResult.UserLastModifiedEmail
                } : null
            };
        }

        public static List<MovieRole> MapToModels(this IEnumerable<MovieRole_GetAll_Result> dbResult)
        {
            var result = new List<MovieRole>();
            if (dbResult == null) return result;
            result.AddRange(dbResult.Select(item => item.MapToModel()));
            return result;
        }

        public static MovieRole MapToModel(this MovieRole_GetRoleOfPersonInMovie_Result dbResult)
        {
            return new MovieRole()
            {
                MovieRoleId = dbResult.MovieRoleId,
                MovieRoleName = dbResult.MovieRoleName,
                MovieRoleDescription = dbResult.MovieRoleDescription,
                DateCreated = dbResult.MovieRoleDateCreated,
                DateLastModified = dbResult.MovieRoleDateLastModified,
                UserCreated = new Model.Users.UserInfo()
                {
                    Id = dbResult.UserCreated.Value,
                    FirstName = dbResult.UserCreatedFirstName,
                    LastName = dbResult.UserCreatedLastName,
                    FullName = dbResult.UserCreatedFullName,
                    Email = dbResult.UserCreatedEmail
                },
                UserLastModified = dbResult.UserLastModified.HasValue ? new Model.Users.UserInfo()
                {
                    Id = dbResult.UserLastModified.Value,
                    FirstName = dbResult.UserLastModifiedFirstName,
                    LastName = dbResult.UserLastModifiedLastName,
                    FullName = dbResult.UserLastModifiedFullName,
                    Email = dbResult.UserLastModifiedEmail
                } : null
            };
        }
        public static MovieRole MapToModel(this Movie_GetRoles_Result dbResult)
        {
            if (dbResult == null) return null;
            return new MovieRole()
            {
                MovieRoleId = dbResult.MovieRoleId,
                MovieRoleName = dbResult.MovieRoleName,
                DateCreated = dbResult.MovieRoleDateCreated,
                UserCreated = new Model.Users.UserInfo()
                {
                    Id = dbResult.UserCreated.Value,
                    FirstName = dbResult.UserCreatedFirstName,
                    LastName = dbResult.UserCreatedLastName,
                    FullName = dbResult.UserCreatedFullName,
                    Email = dbResult.UserCreatedEmail
                },
                DateLastModified = dbResult.MovieRoleDateModified,
                UserLastModified = dbResult.UserLastModified.HasValue ? new Model.Users.UserInfo()
                {
                    Id = dbResult.UserLastModified.Value,
                    FirstName = dbResult.UserLastModifiedFirstName,
                    LastName = dbResult.UserLastModifiedLastName,
                    FullName = dbResult.UserLastModifiedFullName,
                    Email = dbResult.UserLastModifiedEmail
                } : null,
                MoviePersonId=dbResult.MoviePersonId,
                MoviePersonName=dbResult.MoviePersonName
            };
        }

        public static List<MovieRole> MapToModels(this IEnumerable<Movie_GetRoles_Result> dbResult)
        {
            var result = new List<MovieRole>();
            if (dbResult == null) return result;
            result.AddRange(dbResult.Select(item => item.MapToModel()));
            return result;
        }

        public static MovieRole MapToModel(this MoviePerson_GetRoles_Result dbResult)
        {
            if (dbResult == null) return null;
            return new MovieRole()
            {
                MovieRoleId = dbResult.MovieRoleId,
                MovieRoleName = dbResult.MovieRoleName,
                DateCreated = dbResult.MovieRoleDateCreated,
                UserCreated = new Model.Users.UserInfo()
                {
                    Id = dbResult.UserCreated.Value,
                    FirstName = dbResult.UserCreatedFirstName,
                    LastName = dbResult.UserCreatedLastName,
                    FullName = dbResult.UserCreatedFullName,
                    Email = dbResult.UserCreatedEmail
                },
                DateLastModified = dbResult.MovieRoleDateModified,
                UserLastModified = dbResult.UserLastModified.HasValue ? new Model.Users.UserInfo()
                {
                    Id = dbResult.UserLastModified.Value,
                    FirstName = dbResult.UserLastModifiedFirstName,
                    LastName = dbResult.UserLastModifiedLastName,
                    FullName = dbResult.UserLastModifiedFullName,
                    Email = dbResult.UserLastModifiedEmail
                } : null,
                MovieId=dbResult.MovieId,
                MovieName=dbResult.MovieName
            };
        }

        public static List<MovieRole> MapToModels(this IEnumerable<MoviePerson_GetRoles_Result> dbResult)
        {
            var result = new List<MovieRole>();
            if (dbResult == null) return result;
            result.AddRange(dbResult.Select(item => item.MapToModel()));
            return result;
        }


        
    }
}
