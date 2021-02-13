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

        public List<Genre> FindGenres(int key)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.Movie_GetGenres(key).MapToModels();
            }
        }

        public bool DeleteGenre(int genreId, int movieId, int userId)//TODO:Might break? Gotta check this later
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                context.Movie_DeleteGenre(movieId, genreId, userId);
                return true;
            }
        }

        public int AddGenre (int genreId, int movieId, int userId)
        {
            using (var context = new AngularJsSampleDbEntities())
            {
                return context.Movie_AddGenre(movieId, genreId, userId);
            }
        }
    }
}
