using AngularJsSample.Model.Genres;
using AngularJsSample.Model.Movies;
using AngularJsSample.Repositories.Validation;
using AngularJsSample.Services.Mapping;
using AngularJsSample.Services.Messaging.Movies.Requests;
using AngularJsSample.Services.Messaging.Movies.Responses;
using System;
using System.Text.RegularExpressions;

namespace AngularJsSample.Services.Impl
{
    public class MovieService : IMovieService
    {
        private IMovieRepository _repository;
        private IGenreRepository _repository2;
        public MovieService(IMovieRepository repository, IGenreRepository repository2)
        {
            _repository = repository;
            _repository2 = repository2;
        }

        

        public AddMoviePersonToMovieResponse AddMoviePersonToMovie(AddMoviePersonToMovieRequest request)
        {
            var response = new AddMoviePersonToMovieResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                _repository.AddMoviePerson(request.UserId, request.MovieId, request.MoviePersonId, request.MovieRoleId);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }
        public DeleteMovieRoleFromMovieResponse DeleteMovieRoleFromMovie(DeleteMovieRoleFromMovieRequest request)
        {
            var response = new DeleteMovieRoleFromMovieResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                _repository.DeleteMoviePerson(request.UserId, request.MovieId, request.MoviePersonId, request.MovieRoleId);
                response.Success = true;
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }
        public FindMovieRolesResponse FindMovieRoles(FindMovieRolesRequest request)
        {
            var response = new FindMovieRolesResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                var roles =  _repository.FindMovieRoles(request.MovieId);
                response.MovieRoles = roles.MapToViews();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
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
                if (_repository.FindBy(request.MovieId) == null) throw new Exception($"Movie {request.MovieId} doesn't exist");
                if (_repository2.FindBy(request.GenreId) == null) throw new Exception($"Genre {request.GenreId} doesn't exist");
                _repository.AddGenre(request.GenreId, request.MovieId, request.UserId);
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
                if (_repository.DeleteGenre(request.GenreId, request.MovieId, request.UserId))
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
                var movie = _repository.FindBy(request.MovieId);
                if (movie == null) throw new Exception($"Movie {request.MovieId} doesn't exist");
                response.Success = _repository.Delete(new Movie() { MovieId = request.MovieId, UserLastModified = new Model.Users.UserInfo() { Id = request.UserId } });
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
                response.Genres = _repository.FindGenres(request.MovieId).MapToViews();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public FindMovieGenresResponse FindMovieGenresLight(FindMovieGenresRequest request)//TODO nice
        {
            var response = new FindMovieGenresResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                response.Genres = _repository.FindGenresLight(request.MovieId).MapToViews();
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
                response.Movies = _repository.FindAll().MapToViews();
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
                response.Movie = _repository.FindBy(request.MovieId).MapToView();
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
                    request.Movie.MapToModel().CheckMovieForInsertOrUpdate();

                    var newId = _repository.Add(request.Movie.MapToModel());
                    response.Success = true;
                    response.Movie = new Messaging.Views.Movies.Movie() { MovieId = newId };
                }
                else if (request.Movie?.MovieId > 0)
                {
                    if (_repository.FindBy(request.Movie.MovieId) == null)
                        throw new Exception($"Movie {request.Movie.MovieId} doesn't exist");

                    request.Movie.MapToModel().CheckMovieForInsertOrUpdate();

                    response.Movie = _repository.Save(request.Movie.MapToModel()).MapToView();
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
    }
}
