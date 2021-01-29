using AngularJsSample.Model.MovieAuthors;
using AngularJsSample.Repositories.DatabaseModel;
using AngularJsSample.Repositories.Mapping;
using System.Collections.Generic;
using System.Linq;

namespace AngularJsSample.Repositories.MovieAuthors
{
    public class MovieAuthorRepository : IMovieAuthorRepository
    {
        public int Add(MovieAuthor item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.MovieAuthor_Insert(item.UserCreated.Id, item.FirstName, item.LastName, item.BirthDate, item.BirthPlace, item.Biography, item.ImdbUrl, item.ImageUrl, item.Popularity);
            }
        }

        public bool Delete(MovieAuthor item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                context.MovieAuthor_Delete(item.Id, item.UserLastModified.Id);//Returns number of rows affected, should always be 1
                return true;
            }
        }

        public List<MovieAuthor> FindAll()
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.MovieAuthor_GetAll().MapToModels();
            }
        }

        public MovieAuthor FindBy(int key)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.MovieAuthor_Get(key).SingleOrDefault().MapToModel();
            }
        }

        public MovieAuthor Save(MovieAuthor item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                context.MovieAuthor_Update(item.Id, item.UserLastModified.Id, item.FirstName, item.LastName, item.BirthDate, item.BirthPlace, item.Biography, item.ImdbUrl, item.ImageUrl, item.Popularity);
                return FindBy(item.Id);
            }

        }
        
    }
}
