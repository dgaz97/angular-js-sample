using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Model.Genres
{
    public interface IGenreRepository:IRepository<Genre, int>
    {
    }
}
