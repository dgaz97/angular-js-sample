using AngularJsSample.Model.Genres;
using AngularJsSample.Model.MoviePersons;
using AngularJsSample.Model.MovieRoles;
using AngularJsSample.Model.Movies;
using AngularJsSample.Services.Mapping;
using AngularJsSample.Services.Messaging.Movies.Requests;
using AngularJsSample.Services.Messaging.Movies.Responses;
using AngularJsSample.Services.Validation;
using System;
using System.Text.RegularExpressions;

namespace AngularJsSample.Services.Impl
{
    public class MovieService : IMovieService
    {
        private IMovieRepository _repository;
        private IGenreRepository _repository2;
        private IMoviePersonRepository _repository3;
        private IMovieRoleRepository _repository4;
        public MovieService(IMovieRepository repository, IGenreRepository repository2, IMoviePersonRepository repository3, IMovieRoleRepository repository4)
        {
            _repository = repository;
            _repository2 = repository2;
            _repository3 = repository3;
            _repository4 = repository4;
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
                request.CheckIfMovieExists(_repository);
                request.CheckIfMoviePersonExists(_repository3);
                request.CheckIfMovieRoleExists(_repository4);

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
                
                request.CheckIfMovieExists(_repository);
                request.CheckIfMoviePersonExists(_repository3);
                request.CheckIfMovieRoleExists(_repository4);

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
                request.CheckIfMovieExists(_repository);

                var roles =  _repository.FindMovieRoles(request.MovieId);
                response.MovieRoles = roles.MapToViews();
                response.Success = true;
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
                request.CheckIfMovieExists(_repository);
                request.CheckIfGenreExists(_repository2);

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
                request.CheckIfMovieExists(_repository);
                request.CheckIfGenreExists(_repository2);

                _repository.DeleteGenre(request.GenreId, request.MovieId, request.UserId);
                response.Success = true;
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
                request.CheckIfMovieExists(_repository);

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
                request.CheckIfMovieExists(_repository);

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

        public FindMovieGenresResponse FindMovieGenresLight(FindMovieGenresRequest request)
        {
            var response = new FindMovieGenresResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                request.CheckIfMovieExists(_repository);

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
                    request.CheckIfMovieExists(_repository);

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
