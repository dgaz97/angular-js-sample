using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Services.Messaging.MovieRoles.Requests
{
    public class GetRoleOfPersonInMovieRequest:RequestBase
    {
        public int MoviePersonId { get; set; }
        public int MovieId { get; set; }
    }
}
