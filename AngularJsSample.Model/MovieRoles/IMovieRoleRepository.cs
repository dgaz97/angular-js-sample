using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Model.MovieRoles
{
    public interface IMovieRoleRepository:IRepository<MovieRole,int>
    {
        MovieRole GetRoleOfPersonInMovie(int MoviePersonId, int MovieId);
    }
}
