using AngularJsSample.Model.MovieAuthors;
using AngularJsSample.Services.Mapping;
using AngularJsSample.Services.Messaging.MovieAuthors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AngularJsSample.Services.Impl
{
    public class MovieAuthorService : IMovieAuthorService
    {
        private IMovieAuthorRepository _repository;
        public MovieAuthorService(IMovieAuthorRepository repository)
        {
            _repository = repository;
        }

        public DeleteMovieAuthorResponse DeleteMovieAuthor(DeleteMovieAuthorRequest request)
        {
            var response = new DeleteMovieAuthorResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                var author = _repository.FindBy(request.Id);
                if(author==null) throw new Exception($"Author {request.Id} doesn't exist");
                response.Success = _repository.Delete(new MovieAuthor() { Id = request.Id, UserLastModified = new Model.Users.UserInfo() {Id=request.UserId } });
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public GetAllMovieAuthorsResponse GetAllMovieAuthors(GetAllMovieAuthorsRequest request)
        {
            var response = new GetAllMovieAuthorsResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };

            try
            {
                response.MovieAuthors = _repository.FindAll().MapToViews();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public GetMovieAuthorResponse GetMovieAuthor(GetMovieAuthorRequest request)
        {
            var response = new GetMovieAuthorResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };

            try
            {
                response.MovieAuthor = _repository.FindBy(request.Id).MapToView();
                response.Success = true;
            }catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public SaveMovieAuthorResponse SaveMovieAuthor(SaveMovieAuthorRequest request)
        {
            var response = new SaveMovieAuthorResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                if (request.MovieAuthor?.Id == 0)
                {
                    if (request.MovieAuthor.Id != 0) throw new Exception("Movie author ID must be null or 0");
                    checkDataForInsertOrUpdate(request.MovieAuthor.MapToModel());
                    response.MovieAuthor = request.MovieAuthor;
                    response.MovieAuthor.Id = _repository.Add(request.MovieAuthor.MapToModel());
                    response.Success = true;
                }
                else if (request.MovieAuthor?.Id > 0)
                {
                    if (request.MovieAuthor.Id == null || request.MovieAuthor.Id <= 0) throw new Exception("Movie author ID can't be null");
                    if (_repository.FindBy(request.MovieAuthor.Id) == null) throw new Exception($"Author {request.MovieAuthor.Id} doesn't exist");
                    checkDataForInsertOrUpdate(request.MovieAuthor.MapToModel());
                    response.MovieAuthor = _repository.Save(request.MovieAuthor.MapToModel()).MapToView();
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Movie author can't be negative";
                }
            }catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        private void checkDataForInsertOrUpdate(MovieAuthor item)
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

            if (item.Popularity == null || item.Popularity <= 0) throw new Exception("Popularity can't be empty, or less than 1");
        }
    }
}
