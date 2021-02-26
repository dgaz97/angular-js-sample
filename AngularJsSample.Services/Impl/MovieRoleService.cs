using AngularJsSample.Model.MovieRoles;
using AngularJsSample.Services.Mapping;
using AngularJsSample.Services.Messaging.MovieRoles.Requests;
using AngularJsSample.Services.Messaging.MovieRoles.Responses;
using AngularJsSample.Services.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Services.Impl
{
    public class MovieRoleService : IMovieRoleService
    {
        private IMovieRoleRepository _repository;

        public MovieRoleService(IMovieRoleRepository repository)
        {
            _repository = repository;
        }

        public DeleteMovieRoleResponse DeleteMovieRole(DeleteMovieRoleRequest request)
        {
            var response = new DeleteMovieRoleResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                request.CheckIfMovieRoleExists(_repository);

                _repository.Delete(new MovieRole() 
                {
                    MovieId = request.MovieRoleId, 
                    UserLastModified = new Model.Users.UserInfo() {
                        Id = request.UserId 
                    } 
                });
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public GetAllMovieRolesResponse GetAllMovieRoles(GetAllMovieRolesRequest request)
        {
            var response = new GetAllMovieRolesResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                var movieRoles = _repository.FindAll();
                response.MovieRoles = movieRoles.MapToViews();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public GetMovieRoleResponse GetMovieRole(GetMovieRoleRequest request)
        {
            var response = new GetMovieRoleResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                var movieRole = _repository.FindBy(request.MovieRoleId);
                response.MovieRole = movieRole.MapToView();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public SaveMovieRoleResponse SaveMovieRole(SaveMovieRoleRequest request)
        {
            var response = new SaveMovieRoleResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                if (request.MovieRole?.MovieRoleId == 0)
                {
                    request.CheckMovieRoleForInsertOrUpdate();

                    var newId = _repository.Add(request.MovieRole.MapToModel());
                    response.MovieRole = new Messaging.Views.MovieRoles.MovieRole()
                    {
                        MovieRoleId = newId
                    };
                    response.Success = true;
                }
                else if (request.MovieRole?.MovieRoleId > 0)
                {
                    request.CheckIfMovieRoleExists(_repository);
                    request.CheckMovieRoleForInsertOrUpdate();

                    response.MovieRole = _repository.Save(request.MovieRole.MapToModel()).MapToView();
                    response.Success = true;
                }
                else
                {
                    throw new Exception("Movie role ID can't be negative");
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public GetRoleOfPersonInMovieResponse GetRoleOfPersonInMovie(GetRoleOfPersonInMovieRequest request)
        {
            var response = new GetRoleOfPersonInMovieResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                var role = _repository.GetRoleOfPersonInMovie(request.MoviePersonId, request.MovieId);
                response.MovieRole = role.MapToView();
                response.Success = true;
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

    }
}
