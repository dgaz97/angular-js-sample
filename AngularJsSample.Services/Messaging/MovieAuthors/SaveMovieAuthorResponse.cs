using AngularJsSample.Services.Messaging.Views.MovieAuthors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Services.Messaging.MovieAuthors
{
    public class SaveMovieAuthorResponse:ResponseBase<SaveMovieAuthorRequest>
    {
        public MovieAuthor MovieAuthor { get; set; }
    }
}
