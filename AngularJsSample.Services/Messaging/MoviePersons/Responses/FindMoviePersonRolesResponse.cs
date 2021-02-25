using AngularJsSample.Services.Messaging.MoviePersons.Requests;
using AngularJsSample.Services.Messaging.Views.MovieRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Services.Messaging.MoviePersons.Responses
{
    public class FindMoviePersonRolesResponse:ResponseBase<FindMoviePersonRolesRequest>
    {
        public List<MovieRole> MovieRoles { get; set; }
    }
}
