using AngularJsSample.Model.MoviePersons;
using AngularJsSample.Repositories.DatabaseModel;
using AngularJsSample.Repositories.Mapping;
using System.Collections.Generic;
using System.Linq;

namespace AngularJsSample.Repositories.MoviePersons
{
    /// <summary>
    /// Repository for movie persons
    /// </summary>
    public class MoviePersonRepository : IMoviePersonRepository
    {
        /// <summary>
        /// Adds Movie person
        /// </summary>
        /// <param name="item">Movie person object</param>
        /// <returns>ID of newly created movie person</returns>
        public int Add(MoviePerson item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.MoviePerson_Insert(item.UserCreated.Id, item.FirstName, item.LastName, item.BirthDate, item.BirthPlace, item.Biography, item.ImdbUrl, item.ImageUrl, item.Popularity).Single().Value;
            }
        }
        /// <summary>
        /// Deletes movie person
        /// </summary>
        /// <param name="item">Movie person object</param>
        /// <returns>true</returns>
        public bool Delete(MoviePerson item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                context.MoviePerson_Delete(item.Id, item.UserLastModified.Id);//Returns number of rows affected, should always be 1
                return true;
            }
        }
        /// <summary>
        /// Gets all movie persons
        /// </summary>
        /// <returns>List of movie persons</returns>
        public List<MoviePerson> FindAll()
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.MoviePerson_GetAll().MapToModels();
            }
        }
        /// <summary>
        /// Gets specific movie person
        /// </summary>
        /// <param name="key">ID of movie person</param>
        /// <returns>Movie person object</returns>
        public MoviePerson FindBy(int key)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.MoviePerson_Get(key).SingleOrDefault().MapToModel();
            }
        }

        /// <summary>
        /// Updates movie person
        /// </summary>
        /// <param name="item">Movie person object, with new data</param>
        /// <returns>Movie person object</returns>
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
