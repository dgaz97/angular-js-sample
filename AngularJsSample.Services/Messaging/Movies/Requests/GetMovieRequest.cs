using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Services.Messaging.Movies.Requests
{
    public class GetMovieRequest: RequestBase
    {
        public int MovieId { get; set; }
    }
}
