using AngularJsSample.Model.MovieRoles;
using AngularJsSample.Services.Messaging.MovieRoles.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Services.Validation
{
    public static class MovieRoleValidation
    {
        public static void CheckIfMovieRoleExists(this DeleteMovieRoleRequest request, IMovieRoleRepository repository)
        {
            var movieRole = repository.FindBy(request.MovieRoleId);
            if (movieRole == null) throw new Exception($"Movie role {request.MovieRoleId} doesn't exist");
        }

        public static void CheckMovieRoleForInsertOrUpdate(this SaveMovieRoleRequest request)
        {
            if (request.MovieRole.MovieRoleName == null || String.IsNullOrWhiteSpace(request.MovieRole.MovieRoleName))
                throw new Exception("Movie role name can't be empty");
            if(request.MovieRole.MovieRoleName.Length>50)
                throw new Exception("Movie role name can't be greater than 50 characters");

            if (request.MovieRole.MovieRoleDescription == null || String.IsNullOrWhiteSpace(request.MovieRole.MovieRoleDescription))
                request.MovieRole.MovieRoleDescription = "";
            if (request.MovieRole.MovieRoleDescription.Length > 1000)
                throw new Exception("Movie role description can't be greater than 1000 characters");
        }

        public static void CheckIfMovieRoleExists(this SaveMovieRoleRequest request, IMovieRoleRepository repository)
        {
            var movieRole = repository.FindBy(request.MovieRole.MovieRoleId);
            if (movieRole == null) throw new Exception($"Movie role {request.MovieRole.MovieRoleId} doesn't exist");
        }
    }
}
