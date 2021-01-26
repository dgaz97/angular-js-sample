using AngularJsSample.Model.MovieAuthors;
using AngularJsSample.Services.Mapping;
using AngularJsSample.Services.Messaging.MovieAuthors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            throw new NotImplementedException();
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
                    response.MovieAuthor = request.MovieAuthor;
                    response.MovieAuthor.Id = _repository.Add(request.MovieAuthor.MapToModel());
                    response.Success = true;
                }
                else if (request.MovieAuthor?.Id > 0)
                {
                    throw new NotImplementedException("Not implemented yet");
                }
                else
                {
                    response.Success = false;
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
