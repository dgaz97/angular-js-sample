using AngularJsSample.Model.MovieRoles;
using AngularJsSample.Repositories.DatabaseModel;
using AngularJsSample.Repositories.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Repositories.MovieRoles
{
    public class MovieRoleRepository : IMovieRoleRepository
    {
        public int Add(MovieRole item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.MovieRole_Insert(item.UserCreated.Id, item.MovieRoleName, item.MovieRoleDescription).Single().Value;
            }
        }

        public bool Delete(MovieRole item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                context.MovieRole_Delete(item.MovieRoleId, item.UserLastModified.Id);
                return true;
            }
        }

        public List<MovieRole> FindAll()
        {
            using(var context = new AngularJsSampleDbEntities())
            {
                return context.MovieRole_GetAll().MapToModels();
            }
        }

        public MovieRole FindBy(int key)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.MovieRole_Get(key).SingleOrDefault().MapToModel();
            }
        }

        public MovieRole GetRoleOfPersonInMovie(int MoviePersonId, int MovieId)
        {
            using(var context = new AngularJsSampleDbEntities())
            {
                return context.MovieRole_GetRoleOfPersonInMovie(MoviePersonId, MovieId).SingleOrDefault().MapToModel();
            }
        }

        public MovieRole Save(MovieRole item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                context.MovieRole_Update(item.MovieRoleId, item.UserLastModified.Id, item.MovieRoleName, item.MovieRoleDescription);
                return FindBy(item.MovieRoleId);
            }
        }
    }
}
