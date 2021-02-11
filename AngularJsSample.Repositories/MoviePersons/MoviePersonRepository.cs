using AngularJsSample.Model.MoviePersons;
using AngularJsSample.Repositories.DatabaseModel;
using AngularJsSample.Repositories.Mapping;
using System.Collections.Generic;
using System.Linq;

namespace AngularJsSample.Repositories.MoviePersons
{
    public class MoviePersonRepository : IMoviePersonRepository
    {
        public int Add(MoviePerson item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.MoviePerson_Insert(item.UserCreated.Id, item.FirstName, item.LastName, item.BirthDate, item.BirthPlace, item.Biography, item.ImdbUrl, item.ImageUrl, item.Popularity);
            }
        }

        public bool Delete(MoviePerson item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                context.MoviePerson_Delete(item.Id, item.UserLastModified.Id);//Returns number of rows affected, should always be 1
                return true;
            }
        }

        public List<MoviePerson> FindAll()
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.MoviePerson_GetAll().MapToModels();
            }
        }

        public MoviePerson FindBy(int key)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.MoviePerson_Get(key).SingleOrDefault().MapToModel();
            }
        }

        public MoviePerson Save(MoviePerson item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                context.MoviePerson_Update(item.Id, item.UserLastModified.Id, item.FirstName, item.LastName, item.BirthDate, item.BirthPlace, item.Biography, item.ImdbUrl, item.ImageUrl, item.Popularity);
                return FindBy(item.Id);
            }

        }
        
    }
}
