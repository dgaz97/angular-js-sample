using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Services.Messaging.MovieAuthors
{
    public class DeleteMovieAuthorRequest:RequestBase
    {
        public int Id { get; set; }
    }
}
