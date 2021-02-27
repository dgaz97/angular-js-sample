using AngularJsSample.Model.MoviePersons;
using AngularJsSample.Model.MovieRoles;
using AngularJsSample.Model.Movies;
using AngularJsSample.Services.Mapping;
using AngularJsSample.Services.Messaging.MoviePersons;
using AngularJsSample.Services.Messaging.MoviePersons.Requests;
using AngularJsSample.Services.Messaging.MoviePersons.Responses;
using AngularJsSample.Services.Validation;
using System;
using System.Text.RegularExpressions;

namespace AngularJsSample.Services.Impl
{
    public class MoviePersonService : IMoviePersonService
    {
        private IMoviePersonRepository _repository;
        private IMovieRepository _repository2;
        private IMovieRoleRepository _repository3;
        public MoviePersonService(IMoviePersonRepository repository, IMovieRepository repository2, IMovieRoleRepository repository3)
        {
            _repository = repository;
            _repository2 = repository2;
            _repository3 = repository3;
        }

        public AddMovieToMoviePersonResponse AddMovieToMoviePerson(AddMovieToMoviePersonRequest request)
        {
            var response = new AddMovieToMoviePersonResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };

            try
            {
                request.CheckIfMoviePersonExists(_repository);
                request.CheckIfMovieExists(_repository2);
                request.CheckIfMovieRoleExists(_repository3);

                _repository.AddMovie(request.UserId, request.MovieId, request.MoviePersonId, request.MovieRoleId);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }

            return response;
        }

        public DeleteMovieFromMoviePersonResponse DeleteMovieFromMoviePerson(DeleteMovieFromMoviePersonRequest request)
        {
            var response = new DeleteMovieFromMoviePersonResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };

            try
            {
                request.CheckIfMoviePersonExists(_repository);
                request.CheckIfMovieExists(_repository2);
                request.CheckIfMovieRoleExists(_repository3);

                _repository.DeleteMovie(request.UserId,request.MovieId,request.MoviePersonId,request.MovieRoleId);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }

            return response;
        }
        public FindMoviePersonRolesResponse FindMoviePersonRoles(FindMoviePersonRolesRequest request)
        {
            var response = new FindMoviePersonRolesResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                var roles = _repository.FindMovies(request.MoviePersonId);
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

        public DeleteMoviePersonResponse DeleteMoviePerson(DeleteMoviePersonRequest request)
        {
            var response = new DeleteMoviePersonResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                request.CheckIfMoviePersonExists(_repository);
                response.Success = _repository.Delete(new MoviePerson() 
                { 
                    Id = request.MoviePersonId,
                    UserLastModified = new Model.Users.UserInfo() 
                    { 
                        Id = request.UserId 
                    } 
                });
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public GetAllMoviePersonsResponse GetAllMoviePersons(GetAllMoviePersonsRequest request)
        {
            var response = new GetAllMoviePersonsResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };

            try
            {
                response.MoviePersons = _repository.FindAll().MapToViews();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }


        public GetMoviePersonResponse GetMoviePerson(GetMoviePersonRequest request)
        {
            var response = new GetMoviePersonResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };

            try
            {
                response.MoviePerson = _repository.FindBy(request.Id).MapToView();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }
        public SaveMoviePersonResponse SaveMoviePerson(SaveMoviePersonRequest request)
        {
            var response = new SaveMoviePersonResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                if (request.MoviePerson?.Id == 0)//create new
                {
                    request.MoviePerson.MapToModel().CheckDataForInsertOrUpdate();

                    var newId = _repository.Add(request.MoviePerson.MapToModel());
                    response.MoviePerson = new Messaging.Views.MoviePersons.MoviePerson() { Id = newId };
                    response.Success = true;
                }
                else if (request.MoviePerson?.Id > 0)//edit
                {
                    request.CheckIfMoviePersonExists(_repository);

                    request.MoviePerson.MapToModel().CheckDataForInsertOrUpdate();

                    response.MoviePerson = _repository.Save(request.MoviePerson.MapToModel()).MapToView();
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Movie person ID can't be negative";
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
