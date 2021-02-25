using AngularJsSample.Services.Messaging.MovieRoles.Requests;
using AngularJsSample.Services.Messaging.Views.MovieRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Services.Messaging.MovieRoles.Responses
{
    public class GetAllMovieRolesResponse:ResponseBase<GetAllMovieRolesRequest>
    {
        public List<MovieRole> MovieRoles { get; set; }
    }
}
