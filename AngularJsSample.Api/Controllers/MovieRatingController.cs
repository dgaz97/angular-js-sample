using AngularJsSample.Api.Helpers;
using AngularJsSample.Api.Mapping.MovieRatings;
using AngularJsSample.Api.Models.MovieRatings;
using AngularJsSample.Api.Models.Movies;
using AngularJsSample.Api.Models.Users;
using AngularJsSample.Services;
using AngularJsSample.Services.Messaging.MovieRatings.Requests;
using System;
using System.Web;
using System.Web.Http;

namespace AngularJsSample.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/movieRatings")]
    public class MovieRatingController: ApiController
    {
        private IMovieRatingService _movieRatingService;
        public MovieRatingController(IMovieRatingService movieRatingService)
        {
            _movieRatingService = movieRatingService;
        }

        /**
         * Dohvaća sve ratinge
         */
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            var request = new GetAllMovieRatingsRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId
            };

            var response = _movieRatingService.GetAllMovieRatings(request);
            if (!response.Success)
                return BadRequest(response.Message);
            return Ok(new { response.MovieRatings });
        }

        /**
         * Dohvaća ratinge za traženi film
         */
        [HttpGet]
        [Route("movie/{id}")]
        public IHttpActionResult GetByMovie(int id)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();
            var request = new GetMovieRatingsByMovieRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                MovieId = id
            };

            var response = _movieRatingService.GetMovieRatingsByMovie(request);
            if (!response.Success)
                return BadRequest(response.Message);
            return Ok(new { response.MovieRatings });
        }

        /**
         * Dohvaća ratinge za traženog korisnika
         */
        [HttpGet]
        [Route("user/{id}")]
        public IHttpActionResult GetByUser(int id)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();
            var request = new GetMovieRatingsByUserRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                RequestedUser = id
            };

            var response = _movieRatingService.GetMovieRatingsByUser(request);
            if (!response.Success)
                return BadRequest(response.Message);
            return Ok(new { response.MovieRatings });
        }

        /**
         * Dohvaća ratinge za film i traženog korisnika
         */
        [HttpGet]
        [Route("{userId}/{movieId}")]
        public IHttpActionResult GetByMovieAndUser(int userId, int movieId)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();
            var request = new GetMovieRatingByMovieAndUserRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                RequestedUser = userId==0?loggedUserId: userId,
                MovieId = movieId
            };

            var response = _movieRatingService.GetMovieRatingByMovieAndUser(request);
            if (!response.Success)
                return BadRequest(response.Message);
            return Ok(new { response.MovieRating });
        }

        /**
         * spremi novi rating
         */
        [HttpPost]
        [Route("{movieId}")]
        public IHttpActionResult Put(int movieId, MovieRatingViewModel movieRating)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();
            movieRating.UserCreated = new UserViewModel() { Id = loggedUserId };
            movieRating.Movie = new MovieViewModel() { MovieId = movieId };
            var request = new SaveMovieRatingRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                MovieRating = movieRating.MapToView()
            };

            var response = _movieRatingService.SaveMovieRating(request);
            if (!response.Success)
                return BadRequest(response.Message);
            return Ok();
        }

    }
}