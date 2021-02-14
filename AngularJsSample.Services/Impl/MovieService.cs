using AngularJsSample.Model.Genres;
using AngularJsSample.Model.Movies;
using AngularJsSample.Services.Mapping;
using AngularJsSample.Services.Messaging.Movies.Requests;
using AngularJsSample.Services.Messaging.Movies.Responses;
using System;
using System.Text.RegularExpressions;

namespace AngularJsSample.Services.Impl
{
    public class MovieService : IMovieService
    {
        private IMovieRepository _repository_;
        private IGenreRepository _repository2_;
        public MovieService(IMovieRepository repository, IGenreRepository repository2)
        {
            _repository_ = repository;
            _repository2_ = repository2;
        }

        public AddGenreToMovieResponse AddGenreToMovie(AddGenreToMovieRequest request)
        {
            var response = new AddGenreToMovieResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                if (_repository_.FindBy(request.MovieId) == null) throw new Exception($"Movie {request.MovieId} doesn't exist");
                if (_repository2_.FindBy(request.GenreId) == null) throw new Exception($"Genre {request.GenreId} doesn't exist");
                _repository_.AddGenre(request.GenreId, request.MovieId, request.UserId);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public DeleteGenreFromMovieResponse DeleteGenreFromMovie(DeleteGenreFromMovieRequest request)
        {
            var response = new DeleteGenreFromMovieResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                if (_repository_.DeleteGenre(request.GenreId, request.MovieId, request.UserId))
                {
                    response.Success = true;
                }
                else throw new Exception($"Movie {request.MovieId} doesn't have genre {request.GenreId}");

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public DeleteMovieResponse DeleteMovie(DeleteMovieRequest request)
        {
            var response = new DeleteMovieResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                var movie = _repository_.FindBy(request.MovieId);
                if (movie == null) throw new Exception($"Movie {request.MovieId} doesn't exist");
                response.Success = _repository_.Delete(new Movie() { MovieId = request.MovieId, UserLastModified = new Model.Users.UserInfo() { Id = request.UserId } });
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public FindMovieGenresResponse FindMovieGenres(FindMovieGenresRequest request)
        {
            var response = new FindMovieGenresResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                response.Genres = _repository_.FindGenres(request.MovieId).MapToViews();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public GetAllMoviesResponse GetAllMovies(GetAllMoviesRequest request)
        {
            var response = new GetAllMoviesResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                response.Movies = _repository_.FindAll().MapToViews();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public GetMovieResponse GetMovie(GetMovieRequest request)
        {
            var response = new GetMovieResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                response.Movie = _repository_.FindBy(request.MovieId).MapToView();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public SaveMovieResponse SaveMovie(SaveMovieRequest request)
        {
            var response = new SaveMovieResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                if (request.Movie?.MovieId == 0)
                {
                    checkDataForInsertOrUpdate(request.Movie);
                    //response.Movie = request.Movie;
                    _repository_.Add(request.Movie.MapToModel());
                    response.Success = true;
                }
                else if (request.Movie?.MovieId > 0)
                {
                    if (_repository_.FindBy(request.Movie.MovieId) == null)
                        throw new Exception($"Movie {request.Movie.MovieId} doesn't exist");
                    checkDataForInsertOrUpdate(request.Movie);
                    response.Movie = _repository_.Save(request.Movie.MapToModel()).MapToView();
                    response.Success = true;
                }
                else
                {
                    throw new Exception("Movie ID can't be negative");
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;


        }

        private void checkDataForInsertOrUpdate(Messaging.Views.Movies.Movie movie)
        {

            if (movie.MovieName == null || String.IsNullOrWhiteSpace(movie.MovieName))
                throw new Exception("Movie name can't be empty");
            if (movie.MovieName.Length > 200)
                throw new Exception("Movie name can't be greater than 200 characters");

            if (movie.MovieDescription == null || String.IsNullOrWhiteSpace(movie.MovieName))
                movie.MovieDescription = "";
            if (movie.MovieDescription.Length > 2000)
                throw new Exception("Movie description can't be greater than 2000 characters");

            if (movie.MovieReleaseDate == DateTime.MinValue || movie.MovieReleaseDate == null)
                throw new Exception("Movie release date can't be empty");

            //Movie rating se izračunava u bazi nakon svakog ratinga, ne upisuje se ovdje

            Regex rxHttp = new Regex(@"^https?:\/\/");
            Regex rxImage = new Regex(@"\.jpg$|\.jpeg$|\.png$|\.gif$");
            if (!String.IsNullOrWhiteSpace(movie.MoviePosterUrl))
            {
                if (movie.MoviePosterUrl.Length > 200)
                    throw new Exception("Movie poster url must be under 200 characters, or empty");
                if (!rxHttp.IsMatch(movie.MoviePosterUrl) || !rxImage.IsMatch(movie.MoviePosterUrl)) throw new Exception("Poster url is invalid");
            }

            Regex rxImdb = new Regex(@"^https?:\/\/(www\.)?imdb.com/");
            if (String.IsNullOrWhiteSpace(movie.MovieImdbUrl))
                throw new Exception("IMDb url can't be empty");
            if (movie.MovieImdbUrl.Length > 100)
                throw new Exception("IMDb url can't be greater than 100 characters");
            if (!rxImdb.IsMatch(movie.MovieImdbUrl)) throw new Exception("IMDb url is invalid");
        }
    }
}
