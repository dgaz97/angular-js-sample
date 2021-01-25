using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Model.MovieAuthors
{
    public interface IMovieAuthorRepository:IRepository<MovieAuthor, int>
    {
    }
}
