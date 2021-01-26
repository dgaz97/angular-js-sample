using AngularJsSample.Api.Helpers;
using AngularJsSample.Api.Mapping.MovieAuthors;
using AngularJsSample.Api.Models;
using AngularJsSample.Services;
using AngularJsSample.Services.Messaging.MovieAuthors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AngularJsSample.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/movieauthors")]
    public class MovieAuthorsController : ApiController
    {
        private IMovieAuthorService _movieAuthorService;

        public MovieAuthorsController(IMovieAuthorService authorService)
        {
            _movieAuthorService = authorService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            var request = new GetAllMovieAuthorsRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId
            };
            var movieAuthorsResponse = _movieAuthorService.GetAllMovieAuthors(request);

            if (!movieAuthorsResponse.Success)
            {
                return BadRequest(movieAuthorsResponse.Message);
            }
            return Ok(new
            {
                authors = movieAuthorsResponse.MovieAuthors.MapToViewModels()
            });
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            var request = new GetMovieAuthorRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                Id = id
            };

            var movieAuthorsResponse = _movieAuthorService.GetMovieAuthor(request);

            if (!movieAuthorsResponse.Success)
            {
                return BadRequest(movieAuthorsResponse.Message);
            }
            return Ok(movieAuthorsResponse.MovieAuthor.MapToViewModel());
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            var request = new DeleteMovieAuthorRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                Id = id
            };

            var movieAuthorsResponse = _movieAuthorService.DeleteMovieAuthor(request);
            if (!movieAuthorsResponse.Success)
            {
                return BadRequest(movieAuthorsResponse.Message);
            }
            return Ok();

        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(MovieAuthorViewModel movieAuthor)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            movieAuthor.UserCreated = new Models.Users.UserViewModel()
            {
                Id = loggedUserId
            };
            //movieAuthor.DateCreated = DateTimeOffset.Now;//Done in database procedure
            var request = new SaveMovieAuthorRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                MovieAuthor = movieAuthor.MapToView()
            };

            var movieAuthorsResponse = _movieAuthorService.SaveMovieAuthor(request);
            if (!movieAuthorsResponse.Success)
            {
                return BadRequest(movieAuthorsResponse.Message);
            }
            return Ok(movieAuthor = movieAuthorsResponse.MovieAuthor.MapToViewModel());
        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Put(int id, MovieAuthorViewModel movieAuthor)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();
            movieAuthor.UserLastModified = new Models.Users.UserViewModel()
            {
                Id = loggedUserId
            };
            movieAuthor.DateLastModified = DateTimeOffset.Now;

            var request = new SaveMovieAuthorRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                MovieAuthor = movieAuthor.MapToView()
            };

            var movieAuthorsResponse = _movieAuthorService.SaveMovieAuthor(request);

            if (!movieAuthorsResponse.Success)
            {
                return BadRequest(movieAuthorsResponse.Message);
            }

            return Ok(movieAuthor = movieAuthorsResponse.MovieAuthor.MapToViewModel());
        }
    }
}