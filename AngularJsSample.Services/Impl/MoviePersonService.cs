using AngularJsSample.Model.MoviePersons;
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
                if (request.MoviePerson?.Id == 0)
                {
                    if (request.MoviePerson.Id != 0) throw new Exception("Movie person ID must be null or 0");
                    checkDataForInsertOrUpdate(request.MoviePerson.MapToModel());
                    response.MoviePerson = request.MoviePerson;
                    response.MoviePerson.Id = _repository.Add(request.MoviePerson.MapToModel());
                    response.Success = true;
                }
                else if (request.MoviePerson?.Id > 0)
                {
                    if (request.MoviePerson.Id == null || request.MoviePerson.Id <= 0) throw new Exception("Movie person ID can't be null");
                    if (_repository.FindBy(request.MoviePerson.Id) == null) throw new Exception($"Person {request.MoviePerson.Id} doesn't exist");
                    checkDataForInsertOrUpdate(request.MoviePerson.MapToModel());
                    response.MoviePerson = _repository.Save(request.MoviePerson.MapToModel()).MapToView();
                    response.Success = true;
                }
                else
                {
                    throw new Exception("Movie person ID can't be negative");
                }
            }catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        private void checkDataForInsertOrUpdate(MoviePerson item)
        {
            if (item.FirstName == null || String.IsNullOrWhiteSpace(item.FirstName)) throw new Exception("First name can't be empty");
            if (item.LastName == null || String.IsNullOrWhiteSpace(item.LastName)) throw new Exception("Last name can't be empty");
            if (item.BirthDate == DateTime.MinValue || item.BirthDate == null) throw new Exception("Birth date can't be empty");
            if (item.BirthPlace == null || String.IsNullOrWhiteSpace(item.BirthPlace)) throw new Exception("Birth place can't be empty");

            if (item.ImdbUrl == null || String.IsNullOrWhiteSpace(item.ImdbUrl)) throw new Exception("IMDb url can't be empty");
            Regex rxImdb = new Regex(@"^https?:\/\/(www\.)?imdb.com/");
            if (!rxImdb.IsMatch(item.ImdbUrl)) throw new Exception("IMDb url is invalid");

            if (!String.IsNullOrWhiteSpace(item.ImageUrl))
            {
                Regex rxHttp = new Regex(@"^https?:\/\/");
                Regex rxImage = new Regex(@"\.jpg$|\.jpeg$|\.png$|\.gif$");
                if (!rxHttp.IsMatch(item.ImageUrl) || !rxImage.IsMatch(item.ImageUrl)) throw new Exception("Image url is invalid");
            }
            //TODO: description must be under 2000 chars, firstname under 50, lastname under 50, birthplace under 50, imdburl under 100, imageurl under 200

            if (item.Popularity == null || item.Popularity <= 0) throw new Exception("Popularity can't be empty, or less than 1");
        }
    }
}
