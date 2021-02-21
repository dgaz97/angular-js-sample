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
    [Authorize]
    [RoutePrefix("api/genres")]
    public class GenreController : ApiController
    {
        private IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        /**
         * Dohvaća sve žanrove
         */
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

        /**
         * Dohvaća jedan žanr
         */
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

        /**
         * Žanru dodaje film
         */
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

        /**
         * Briše žanr
         */
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

        /**
         * Žanru briše film
         */
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

        /**
         * Dohvati sve filmove koji imaju traženi žanr
         */
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
        /**
         * Dodaje novi žanr
         */
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

        /**
         * Ažurira postojeći žanr
         */
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