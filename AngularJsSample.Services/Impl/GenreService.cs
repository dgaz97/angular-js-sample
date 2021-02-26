using AngularJsSample.Model.Genres;
using AngularJsSample.Model.Movies;
using AngularJsSample.Services.Mapping;
using AngularJsSample.Services.Messaging.Genres.Requests;
using AngularJsSample.Services.Messaging.Genres.Responses;
using AngularJsSample.Services.Validation;
using System;

namespace AngularJsSample.Services.Impl
{
    public class GenreService : IGenreService
    {
        private IGenreRepository _repository;
        private IMovieRepository _repository2;
        public GenreService(IGenreRepository repository, IMovieRepository repository2)
        {
            _repository = repository;
            _repository2 = repository2;
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
                request.CheckIfGenreExists(_repository);
                request.CheckIfMovieExists(_repository2);

                _repository.AddMovie(request.GenreId, request.MovieId, request.UserId);
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
                request.CheckIfGenreExists(_repository);

                response.Success = _repository.Delete(new Genre() { GenreId = request.GenreId, UserLastModified = new Model.Users.UserInfo() { Id = request.UserId } });
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
                request.CheckIfGenreExists(_repository);
                request.CheckIfMovieExists(_repository2);

                _repository.DeleteMovie(request.GenreId, request.MovieId, request.UserId);
                response.Success = true;

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
                response.Movies = _repository.FindMovies(request.GenreId).MapToViews();
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
                response.Genres = _repository.FindAll().MapToViews();
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
                response.Genre = _repository.FindBy(request.GenreId).MapToView();
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
                ResponseToken = Guid.NewGuid(),
            };
            try
            {
                if (request.Genre?.GenreId == 0)
                {
                    request.Genre.MapToModel().CheckGenreForInsertOrUpdate();
                    
                    var newId = _repository.Add(request.Genre.MapToModel());

                    response.Success = true;
                    response.Genre = new Messaging.Views.Genres.Genre() { GenreId = newId };
                }
                else if (request.Genre?.GenreId > 0)
                {
                    request.CheckIfGenreExists(_repository);

                    request.Genre.MapToModel().CheckGenreForInsertOrUpdate();

                    response.Genre = _repository.Save(request.Genre.MapToModel()).MapToView();
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
