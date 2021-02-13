using System.Collections.Generic;
using System.Linq;
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

        public List<Movie> FindMovies(int genreId)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.Genre_GetMovies(genreId).MapToModels();
            }
        }

        public bool DeleteMovie(int genreId, int movieId, int userId)//TODO:Might break? Gotta check this later
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                if(context.Genre_DeleteMovie(movieId, genreId, userId)==1)return true;
                else return false;
            }
        }

        public int AddMovie (int genreId, int movieId, int userId)//TODO: might need to return null?
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.Genre_AddMovie(movieId, genreId, userId);
            }
        }

    }
}
