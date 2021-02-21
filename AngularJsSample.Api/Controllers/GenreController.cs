using AngularJsSample.Api.Helpers;
using AngularJsSample.Api.Mapping.Genres;
using AngularJsSample.Api.Models.Genres;
using AngularJsSample.Services;
using AngularJsSample.Services.Messaging.Genres.Requests;
using System;
using System.Web;
using System.Web.Http;

namespace AngularJsSample.Api.Controllers
{
    /// <summary>
    /// Genre API controller
    /// </summary>
    [Authorize]
    [RoutePrefix("api/genres")]
    public class GenreController : ApiController
    {
        private IGenreService _genreService;
        /// <summary>
        /// Constructor, for use with AutoFac, that accepts an IGenreService implementation
        /// </summary>
        /// <param name="genreService"> An IGenreService implementation</param>
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        /// <summary>
        /// Gets a list of all genres
        /// </summary>
        /// <returns>Ok response with a list of all genres, or BadRequest with error message</returns>

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            var request = new GetAllGenresRequest() {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId
            };

            var response = _genreService.GetAllGenres(request);
            if (!response.Success)
                return BadRequest(response.Message);
            return Ok(new
            {
                genres = response.Genres.MapToViewModels()
            });
        }

        /// <summary>
        /// Gets one genre, by ID
        /// </summary>
        /// <param name="id">ID of genre that we're getting</param>
        /// <returns>Ok response with the genre that we're getting, or BadRequest with error message</returns>
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            var request = new GetGenreRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                GenreId = id
            };

            var response = _genreService.GetGenre(request);
            if (!response.Success)
                return BadRequest(response.Message);
            return Ok(response.Genre.MapToViewModel());
        }

        /// <summary>
        /// Adds genre to movie
        /// </summary>
        /// <param name="genreId">Genre that we're adding to the movie</param>
        /// <param name="movieId">Movie that we're adding the genre to</param>
        /// <returns>Empty Ok response, or BadRequest with error message</returns>
        [HttpPost]
        [Route("{genreId}/{movieId}")]
        public IHttpActionResult Post(int genreId, int movieId)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            var request = new AddMovieToGenreRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                GenreId = genreId,
                MovieId = movieId
            };

            var response = _genreService.AddMovieToGenre(request);
            if (!response.Success)
                return BadRequest(response.Message);
            return Ok();
        }

        /// <summary>
        /// Deletes genre
        /// </summary>
        /// <param name="id">ID of genre that we're deleting</param>
        /// <returns>Empty Ok response, or BadRequest with error message</returns>
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();
            var request = new DeleteGenreRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                GenreId = id
            };

            var response = _genreService.DeleteGenre(request);
            if (!response.Success)
                return BadRequest(response.Message);
            return Ok();
        }

        /// <summary>
        /// Deletes genre from movie
        /// </summary>
        /// <param name="genreId">Genre that we're deleting from movie</param>
        /// <param name="movieId">Movie that we're deleting the genre from</param>
        /// <returns>Empty Ok response, or BadRequest with error message</returns>
        [HttpDelete]
        [Route("{genreId}/{movieId}")]
        public IHttpActionResult Delete(int genreId, int movieId)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();
            var request = new DeleteMovieFromGenreRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                GenreId = genreId,
                MovieId=movieId
            };

            var response = _genreService.DeleteMovieFromGenre(request);
            if (!response.Success)
                return BadRequest(response.Message);
            return Ok();
        }

        /// <summary>
        /// Gets all movies that have the requested genre
        /// </summary>
        /// <param name="id">ID of genre</param>
        /// <returns>Ok response with a list of movies, or BadRequest with error message</returns>
        [HttpGet]
        [Route("movies/{id}")]
        public IHttpActionResult GetMovies(int id)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();
            var request = new FindGenreMoviesRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                GenreId = id
            };

            var response = _genreService.FindGenreMovies(request);
            if (!response.Success)
                return BadRequest(response.Message);
            return Ok(new { response.Movies });
        }
        /// <summary>
        /// Adds a new genre
        /// </summary>
        /// <param name="genre">Genre object</param>
        /// <returns>Ok response with the genre object (only contains newly created ID), or BadRequest with error message</returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(GenreViewModel genre)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();
            
            genre.UserCreated = new Models.Users.UserViewModel()
            {
                Id = loggedUserId
            };
            var request = new SaveGenreRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                Genre = genre.MapToView()
            };

            var response = _genreService.SaveGenre(request);
            if (!response.Success)
                return BadRequest(response.Message);
            return Ok(response.Genre.MapToViewModel());
        }

        /// <summary>
        /// Updates genre with ID
        /// </summary>
        /// <param name="id">ID of genre that we're updating</param>
        /// <param name="genre">Genre object with new data</param>
        /// <returns>Ok response with the genre object, or BadRequest with error message</returns>

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Put(int id, GenreViewModel genre)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();
            
            genre.UserLastModified = new Models.Users.UserViewModel()
            {
                Id = loggedUserId
            };
            genre.GenreId = id;

            var request = new SaveGenreRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                Genre = genre.MapToView()
            };

            var response = _genreService.SaveGenre(request);
            if (!response.Success)
                return BadRequest(response.Message);
            return Ok(response.Genre.MapToViewModel());
        }


    }
}