using AngularJsSample.Api.Helpers;
using AngularJsSample.Api.Mapping.MoviePersons;
using AngularJsSample.Api.Models;
using AngularJsSample.Api.Models.MovieRoles;
using AngularJsSample.Services;
using AngularJsSample.Services.Messaging.MoviePersons;
using AngularJsSample.Services.Messaging.MoviePersons.Requests;
using System;
using System.Web;
using System.Web.Http;

namespace AngularJsSample.Api.Controllers
{
    /// <summary>
    /// Movie person API controller
    /// </summary>
    [Authorize]
    [RoutePrefix("api/moviepersons")]
    public class MoviePersonsController : ApiController
    {
        private IMoviePersonService _moviePersonService;

        /// <summary>
        /// Constructor, for use with AutoFac, that accepts an IMoviePersonService implementation
        /// </summary>
        /// <param name="personService">IMoviePersonService implementation</param>
        public MoviePersonsController(IMoviePersonService personService)
        {
            _moviePersonService = personService;
        }

        /// <summary>
        /// Gets a list of all movie persons
        /// </summary>
        /// <returns>Ok response with a list of all movie persons, or BadRequest with error message</returns>
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

        /// <summary>
        /// Gets one movie person, by ID
        /// </summary>
        /// <param name="id">ID of movie person tat we're getting</param>
        /// <returns>Ok response with the movie person that we're getting, or BadRequest with error message</returns>
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

        /// <summary>
        /// Deletes movie person
        /// </summary>
        /// <param name="id">ID of movie person</param>
        /// <returns>Empty Ok response, or BadRequest with error message</returns>
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            var request = new DeleteMoviePersonRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                MoviePersonId = id
            };

            var moviePersonsResponse = _moviePersonService.DeleteMoviePerson(request);
            if (!moviePersonsResponse.Success)
            {
                return BadRequest(moviePersonsResponse.Message);
            }
            return Ok();

        }

        /// <summary>
        /// Adds new movie person
        /// </summary>
        /// <param name="moviePerson">Movie person object</param>
        /// <returns>Ok response with a MoviePerson object (only contains newly created ID), or BadRequest with error message</returns>
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
            return Ok(moviePersonsResponse.MoviePerson);
        }

        /// <summary>
        /// Updates movie person with ID
        /// </summary>
        /// <param name="moviePerson">Movie person object with new data</param>
        /// <returns>Ok response with the movie person object, or BadRequest with error message</returns>
        [HttpPut]
        [Route("")]
        public IHttpActionResult Put(MoviePersonViewModel moviePerson)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();
            moviePerson.UserLastModified = new Models.Users.UserViewModel()
            {
                Id = loggedUserId
            };
            moviePerson.DateLastModified = DateTimeOffset.Now;
            //moviePerson.Id = id;

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

            return Ok(moviePersonsResponse.MoviePerson.MapToViewModel());
        }

        [HttpGet]
        [Route("roles/{id}")]
        public IHttpActionResult GetRoles(int id)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            var request = new FindMoviePersonRolesRequest()
            {
                UserId = loggedUserId,
                RequestToken = Guid.NewGuid(),
                MoviePersonId = id
            };

            var response = _moviePersonService.FindMoviePersonRoles(request);

            if (!response.Success)
                return BadRequest(response.Message);
            return Ok(new { movieRoles = response.MovieRoles });

        }

        [HttpPost]
        [Route("roles")]
        public IHttpActionResult PostRole(MovieRoleViewModel movieRole)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            var request = new AddMovieToMoviePersonRequest()
            {
                UserId = loggedUserId,
                RequestToken = Guid.NewGuid(),
                MovieId = movieRole.MovieId.GetValueOrDefault(),
                MovieRoleId = movieRole.MovieRoleId,
                MoviePersonId = movieRole.MoviePersonId.GetValueOrDefault()
            };

            var response = _moviePersonService.AddMovieToMoviePerson(request);

            if (!response.Success)
                return BadRequest(response.Message);
            return Ok();
        }

        [HttpDelete]
        [Route("roles")]
        public IHttpActionResult DeleteRole(MovieRoleViewModel movieRole)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            var request = new DeleteMovieFromMoviePersonRequest()
            {
                UserId = loggedUserId,
                RequestToken = Guid.NewGuid(),
                MovieId = movieRole.MovieId.GetValueOrDefault(),
                MovieRoleId = movieRole.MovieRoleId,
                MoviePersonId = movieRole.MoviePersonId.GetValueOrDefault()
            };

            var response = _moviePersonService.DeleteMovieFromMoviePerson(request);

            if (!response.Success)
                return BadRequest(response.Message);
            return Ok();
        }
    }
}