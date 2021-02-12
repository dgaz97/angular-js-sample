using AngularJsSample.Services.Messaging.Movies.Requests;
using AngularJsSample.Services.Messaging.Views.Genres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Services.Messaging.Movies.Responses
{
    public class FindMovieGenresResponse:ResponseBase<FindMovieGenresRequest>
    {
        public List<Genre> Genres { get; set; }
    }
}
