using AngularJsSample.Services.Messaging.MovieRoles.Requests;
using AngularJsSample.Services.Messaging.MovieRoles.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Services
{
    public interface IMovieRoleService
    {
        DeleteMovieRoleResponse DeleteMovieRole(DeleteMovieRoleRequest request);
        GetAllMovieRolesResponse GetAllMovieRoles(GetAllMovieRolesRequest request);
        GetMovieRoleResponse GetMovieRole(GetMovieRoleRequest request);
        SaveMovieRoleResponse SaveMovieRole(SaveMovieRoleRequest request);
    }
}
