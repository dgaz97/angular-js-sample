using AngularJsSample.Services.Messaging.Movies.Requests;
using AngularJsSample.Services.Messaging.Views.MovieRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Services.Messaging.Movies.Responses
{
    public class FindMovieRolesResponse:ResponseBase<FindMovieRolesRequest>
    {
        public List<MovieRole> MovieRoles { get; set; }
    }
}
