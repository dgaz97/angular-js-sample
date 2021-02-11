using AngularJsSample.Services.Messaging.Views.MoviePersons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Services.Messaging.MoviePersons
{
    public class SaveMoviePersonResponse:ResponseBase<SaveMoviePersonRequest>
    {
        public MoviePerson MoviePerson { get; set; }
    }
}
