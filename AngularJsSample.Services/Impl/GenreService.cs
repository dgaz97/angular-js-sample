using AngularJsSample.Model.Genres;
using AngularJsSample.Model.Movies;
using AngularJsSample.Services.Mapping;
using AngularJsSample.Services.Messaging.Genres.Requests;
using AngularJsSample.Services.Messaging.Genres.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Services.Impl
{
    public class GenreService : IGenreService
    {
        private IGenreRepository _repository_;
        private IMovieRepository _repository2_;
        public GenreService(IGenreRepository repository, IMovieRepository repository2)
        {
            _repository_ = repository;
            _repository2_ = repository2;
        }
        public AddMovieToGenreResponse AddMovieToGenre(AddMovieToGenreRequest request)
        {
            var response = new AddMovieToGenreResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                if (_repository_.FindBy(request.GenreId) == null) throw new Exception($"Genre {request.GenreId} doesn't exist");
                if (_repository2_.FindBy(request.MovieId) == null) throw new Exception($"Movie {request.MovieId} doesn't exist");
                _repository_.AddMovie(request.GenreId, request.MovieId, request.UserId);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public DeleteGenreResponse DeleteGenre(DeleteGenreRequest request)
        {
            var response = new DeleteGenreResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                var genre = _repository_.FindBy(request.GenreId);
                if (genre == null) throw new Exception($"Genre {request.GenreId} doesn't exist");
                response.Success = _repository_.Delete(new Genre() { GenreId = request.GenreId, UserLastModified = new Model.Users.UserInfo() { Id = request.UserId } });
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public DeleteMovieFromGenreResponse DeleteMovieFromGenre(DeleteMovieFromGenreRequest request)
        {
            var response = new DeleteMovieFromGenreResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                if (_repository_.DeleteMovie(request.GenreId, request.MovieId, request.UserId))
                {
                    response.Success = true;
                }
                else throw new Exception($"Genre {request.GenreId} doesn't have movie {request.MovieId}");
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public FindGenreMoviesResponse FindGenreMovies(FindGenreMoviesRequest request)
        {
            var response = new FindGenreMoviesResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                response.Movies = _repository_.FindMovies(request.GenreId).MapToViews();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public GetAllGenresResponse GetAllGenres(GetAllGenresRequest request)
        {
            var response = new GetAllGenresResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                response.Genres = _repository_.FindAll().MapToViews();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public GetGenreResponse GetGenre(GetGenreRequest request)
        {
            var response = new GetGenreResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                response.Genre = _repository_.FindBy(request.GenreId).MapToView();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public SaveGenreResponse SaveGenre(SaveGenreRequest request)
        {
            var response = new SaveGenreResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                if (request.Genre.GenreId == 0)
                {
                    if (request.Genre.Name == null || String.IsNullOrWhiteSpace(request.Genre.Name)) throw new Exception("Genre name can't be empty");
                    if (request.Genre.Name.Length > 50)
                        throw new Exception("Genre name can't be greater than 50 characters");
                    if (request.Genre.Description == null) request.Genre.Description = "";
                    if (request.Genre.Description.Length > 1000)
                        throw new Exception("Genre description can't be greater than 1000 characters");
                    response.Genre = request.Genre;
                    response.Genre.GenreId = _repository_.Add(request.Genre.MapToModel());
                    response.Success = true;
                }
                else if (request.Genre.GenreId > 0)
                {
                    if (_repository_.FindBy(request.Genre.GenreId) == null) throw new Exception($"Genre {request.Genre.GenreId} doesn't exist");
                    if (request.Genre.Name == null || String.IsNullOrWhiteSpace(request.Genre.Name)) throw new Exception("Genre name can't be empty");
                    if (request.Genre.Name.Length > 50)
                        throw new Exception("Genre name can't be greater than 50 characters");
                    if (request.Genre.Description == null) request.Genre.Description = "";
                    if (request.Genre.Description.Length > 1000)
                        throw new Exception("Genre description can't be greater than 1000 characters");
                    response.Genre = _repository_.Save(request.Genre.MapToModel()).MapToView();
                    response.Success = true;
                }
                else
                {
                    throw new Exception("Genre ID can't be negative");
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }
    }
}
