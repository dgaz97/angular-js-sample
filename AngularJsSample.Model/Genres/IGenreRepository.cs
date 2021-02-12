using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Model.Genres
{
    interface IGenreRepository:IRepository<Genre, int>
    {
    }
}
