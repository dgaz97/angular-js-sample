using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngularJsSample.Model.Genres;
using AngularJsSample.Model.Movies;
using AngularJsSample.Repositories.DatabaseModel;
using AngularJsSample.Repositories.Mapping;

namespace AngularJsSample.Repositories.Genres
{
    public class GenreRepository : IGenreRepository
    {
        public int Add(Genre item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.Genre_Insert(item.UserCreated.Id, item.Name, item.Description);
            }
        }

        public bool Delete(Genre item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                context.Genre_Delete(item.GenreId, item.UserLastModified.Id);
                return true;
            }
        }

        public List<Genre> FindAll()
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.Genre_GetAll().MapToModels();
            }
        }

        public Genre FindBy(int key)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.Genre_Get(key).SingleOrDefault().MapToModel();
            }
        }

        public Genre Save(Genre item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                context.Genre_Update(item.GenreId, item.UserLastModified.Id, item.Name, item.Description);
                return FindBy(item.GenreId);
            }
        }

        public List<Movie> FindMovies(Genre item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.Genre_GetMovies(item.GenreId).MapToModels();
            }
        }

        public bool DeleteMovie(Genre item, Movie item2, UserInfo item3)//TODO:Might break? Gotta check this later
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                context.Genre_DeleteMovie(item.GenreId, item2.MovieId, item3.Id);
                return true;
            }
        }

        public int AddMovie (Genre item, Movie item2, UserInfo item3)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.Genre_AddMovie(item2.MovieId, item.GenreId, item3.Id);
            }
        }
    }
}
