using AngularJsSample.Model.Genres;
using AngularJsSample.Model.Movies;
using AngularJsSample.Repositories.DatabaseModel;
using AngularJsSample.Repositories.Mapping;
using System.Collections.Generic;
using System.Linq;

namespace AngularJsSample.Repositories.Movies
{
    public class MovieRepository : IMovieRepository
    {
        public int Add(Movie item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.Movie_Insert(item.UserCreated.Id, item.MovieName, item.MovieDescription, item.MovieReleaseDate, item.MoviePosterUrl, item.MovieImdbUrl);
            }
        }

        public bool Delete(Movie item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                context.Movie_Delete(item.UserLastModified.Id, item.MovieId);
                return true;
            }
        }

        public List<Movie> FindAll()
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.Movie_GetAll().MapToModels();
            }
        }

        public Movie FindBy(int key)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.Movie_Get(key).SingleOrDefault().MapToModel();
            }
        }

        public Movie Save(Movie item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                context.Movie_Update(item.UserLastModified.Id, item.MovieId, item.MovieName, item.MovieDescription, item.MovieReleaseDate, item.MoviePosterUrl, item.MovieImdbUrl);
                return FindBy(item.MovieId);
            }
        }

        public List<Genre> FindGenres(Movie item)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.Movie_GetGenres(item.MovieId).MapToModels();
            }
        }

        public bool DeleteGenre(Genre item, Movie item2, UserInfo item3)//TODO:Might break? Gotta check this later
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                context.Movie_DeleteGenre(item2.MovieId, item.GenreId, item3.Id);
                return true;
            }
        }

        public int AddGenre (Genre item, Movie item2, UserInfo item3)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.Movie_AddGenre(item2.MovieId, item.GenreId, item3.Id);
            }
        }
    }
}
