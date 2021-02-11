using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Model.MoviePersons
{
    public interface IMoviePersonRepository:IRepository<MoviePerson, int>
    {
    }
}
