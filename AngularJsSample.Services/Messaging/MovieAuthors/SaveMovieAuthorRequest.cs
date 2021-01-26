using AngularJsSample.Services.Messaging.Views.MovieAuthors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Services.Messaging.MovieAuthors
{
    public class SaveMovieAuthorRequest:RequestBase
    {
        public MovieAuthor MovieAuthor { get; set; }
    }
}
