using AngularJsSample.Model.MovieRoles;
using AngularJsSample.Model.Movies;
using System.Collections.Generic;

namespace AngularJsSample.Model.MoviePersons
{
    /// <summary>
    /// Interface for Movie person repository
    /// </summary>
    public interface IMoviePersonRepository:IRepository<MoviePerson, int>
    {
        bool AddMovie(int UserId, int MovieId, int MoviePersonId, int MovieRoleId);
        bool DeleteMovie(int UserId, int MovieId, int MoviePersonId, int MovieRoleId);
        List<MovieRole> FindMovies(int key);
    }
}
