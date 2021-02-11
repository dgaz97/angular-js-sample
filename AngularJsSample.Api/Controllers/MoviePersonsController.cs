using AngularJsSample.Api.Helpers;
using AngularJsSample.Api.Mapping.MoviePersons;
using AngularJsSample.Api.Models;
using AngularJsSample.Services;
using AngularJsSample.Services.Messaging.MoviePersons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AngularJsSample.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/moviepersons")]
    public class MoviePersonsController : ApiController
    {
        private IMoviePersonService _moviePersonService;

        public MoviePersonsController(IMoviePersonService personService)
        {
            _moviePersonService = personService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            var request = new GetAllMoviePersonsRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId
            };
            var moviePersonsResponse = _moviePersonService.GetAllMoviePersons(request);

            if (!moviePersonsResponse.Success)
            {
                return BadRequest(moviePersonsResponse.Message);
            }
            return Ok(new
            {
                persons = moviePersonsResponse.MoviePersons.MapToViewModels()
            });
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            var request = new GetMoviePersonRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                Id = id
            };

            var moviePersonsResponse = _moviePersonService.GetMoviePerson(request);

            if (!moviePersonsResponse.Success)
            {
                return BadRequest(moviePersonsResponse.Message);
            }
            return Ok(moviePersonsResponse.MoviePerson.MapToViewModel());
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            var request = new DeleteMoviePersonRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                Id = id
            };

            var moviePersonsResponse = _moviePersonService.DeleteMoviePerson(request);
            if (!moviePersonsResponse.Success)
            {
                return BadRequest(moviePersonsResponse.Message);
            }
            return Ok();

        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(MoviePersonViewModel moviePerson)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            moviePerson.UserCreated = new Models.Users.UserViewModel()
            {
                Id = loggedUserId
            };
            //moviePerson.DateCreated = DateTimeOffset.Now;//Done in database procedure
            var request = new SaveMoviePersonRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                MoviePerson = moviePerson.MapToView()
            };

            var moviePersonsResponse = _moviePersonService.SaveMoviePerson(request);
            if (!moviePersonsResponse.Success)
            {
                return BadRequest(moviePersonsResponse.Message);
            }
            return Ok(moviePerson = moviePersonsResponse.MoviePerson.MapToViewModel());
        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Put(int id, MoviePersonViewModel moviePerson)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();
            moviePerson.UserLastModified = new Models.Users.UserViewModel()
            {
                Id = loggedUserId
            };
            moviePerson.DateLastModified = DateTimeOffset.Now;
            moviePerson.Id = id;

            var request = new SaveMoviePersonRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                MoviePerson = moviePerson.MapToView()
            };

            var moviePersonsResponse = _moviePersonService.SaveMoviePerson(request);

            if (!moviePersonsResponse.Success)
            {
                return BadRequest(moviePersonsResponse.Message);
            }

            return Ok(moviePerson = moviePersonsResponse.MoviePerson.MapToViewModel());
        }
    }
}