using AngularJsSample.Model.MoviePersons;
using AngularJsSample.Repositories.Validation;
using AngularJsSample.Services.Mapping;
using AngularJsSample.Services.Messaging.MoviePersons;
using System;
using System.Text.RegularExpressions;

namespace AngularJsSample.Services.Impl
{
    public class MoviePersonService : IMoviePersonService
    {
        private IMoviePersonRepository _repository;
        public MoviePersonService(IMoviePersonRepository repository)
        {
            _repository = repository;
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
                var person = _repository.FindBy(request.Id);
                if(person==null) throw new Exception($"Person {request.Id} doesn't exist");
                response.Success = _repository.Delete(new MoviePerson() { Id = request.Id, UserLastModified = new Model.Users.UserInfo() {Id=request.UserId } });
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
            }catch(Exception ex)
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
                    if (request.MoviePerson.Id != 0) throw new Exception("Movie person ID must be null or 0");
                    request.MoviePerson.MapToModel().CheckDataForInsertOrUpdate();

                    var newId = _repository.Add(request.MoviePerson.MapToModel());
                    response.MoviePerson = new Messaging.Views.MoviePersons.MoviePerson() { Id = newId };
                    response.Success = true;
                }
                else if (request.MoviePerson?.Id > 0)//edit
                {
                    if (request.MoviePerson.Id <= 0) throw new Exception("Movie person ID can't be null");
                    if (_repository.FindBy(request.MoviePerson.Id) == null) throw new Exception($"Person {request.MoviePerson.Id} doesn't exist");

                    request.MoviePerson.MapToModel().CheckDataForInsertOrUpdate();

                    response.MoviePerson = _repository.Save(request.MoviePerson.MapToModel()).MapToView();
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Movie person ID can't be negative";
                }
            }catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

    }
}
