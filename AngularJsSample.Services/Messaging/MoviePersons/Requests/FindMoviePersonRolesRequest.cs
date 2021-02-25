using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Services.Messaging.MoviePersons.Requests
{
    public class FindMoviePersonRolesRequest:RequestBase
    {
        public int MoviePersonId { get; set; }
    }
}
