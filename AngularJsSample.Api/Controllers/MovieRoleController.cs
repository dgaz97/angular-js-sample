using AngularJsSample.Api.Helpers;
using AngularJsSample.Api.Mapping.MovieRoles;
using AngularJsSample.Api.Models.MovieRoles;
using AngularJsSample.Services;
using AngularJsSample.Services.Messaging.MovieRoles.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AngularJsSample.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/movieRoles")]
    public class MovieRoleController:ApiController
    {
        private IMovieRoleService _movieRoleService;
        public MovieRoleController(IMovieRoleService movieRoleService)
        {
            _movieRoleService = movieRoleService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            var request = new GetAllMovieRolesRequest()
            {
                UserId = loggedUserId,
                RequestToken = Guid.NewGuid()
            };
            var response = _movieRoleService.GetAllMovieRoles(request);
            if (!response.Success)
                return BadRequest(response.Message);
            return Ok(new { movieRoles = response.MovieRoles.MapToViewModels() });
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            var request = new GetMovieRoleRequest()
            {
                UserId = loggedUserId,
                RequestToken = Guid.NewGuid(),
                MovieRoleId = id
            };
            var response = _movieRoleService.GetMovieRole(request);
            if (!response.Success)
                return BadRequest(response.Message);
            return Ok(response.MovieRole.MapToViewModel());
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(MovieRoleViewModel movieRole)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            movieRole.UserCreated = new Models.Users.UserViewModel()
            {
                Id = loggedUserId
            };

            var request = new SaveMovieRoleRequest()
            {
                UserId = loggedUserId,
                RequestToken = Guid.NewGuid(),
                MovieRole = movieRole.MapToView()
            };
            var response = _movieRoleService.SaveMovieRole(request);
            if (!response.Success)
                return BadRequest(response.Message);
            return Ok(response.MovieRole.MapToViewModel());
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult Put(MovieRoleViewModel movieRole)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();
            movieRole.UserLastModified = new Models.Users.UserViewModel()
            {
                Id = loggedUserId
            };
            var request = new SaveMovieRoleRequest()
            {
                UserId = loggedUserId,
                RequestToken = Guid.NewGuid(),
                MovieRole = movieRole.MapToView()
            };
            var response = _movieRoleService.SaveMovieRole(request);

            if (!response.Success)
                return BadRequest(response.Message);
            return Ok(response.MovieRole.MapToViewModel());
        }
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            var request = new DeleteMovieRoleRequest()
            {
                UserId = loggedUserId,
                RequestToken = Guid.NewGuid(),
                MovieRoleId = id
            };

            var response = _movieRoleService.DeleteMovieRole(request);
            if (!response.Success)
                return BadRequest(response.Message);
            return Ok();
        }

        [HttpGet]
        [Route("{movieId}/{moviePersonId}")]
        public IHttpActionResult Get(int movieId, int moviePersonId)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            var request = new GetRoleOfPersonInMovieRequest()
            {
                UserId = loggedUserId,
                RequestToken = Guid.NewGuid(),
                MovieId = movieId,
                MoviePersonId = moviePersonId
            };

            var response = _movieRoleService.GetRoleOfPersonInMovie(request);
            if (!response.Success)
                return BadRequest(response.Message);
            return Ok(response.MovieRole);
        }
    }
}