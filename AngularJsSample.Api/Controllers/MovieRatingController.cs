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
    /// <summary>
    /// Movie rating API controller
    /// </summary>
    [Authorize]
    [RoutePrefix("api/movieRatings")]
    public class MovieRatingController: ApiController
    {
        private IMovieRatingService _movieRatingService;
        /// <summary>
        /// Constructor, for use with AutoFac, that accepts an IMovieRatingService implementation
        /// </summary>
        /// <param name="movieRatingService">IMovieRatingService implementation</param>
        public MovieRatingController(IMovieRatingService movieRatingService)
        {
            _movieRatingService = movieRatingService;
        }

        /// <summary>
        /// Gets a list of all movie ratings
        /// </summary>
        /// <returns>Ok response with a list of all movie ratings, or BadRequest with error message</returns>
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

        /// <summary>
        /// Gets ratings for movie
        /// </summary>
        /// <param name="id">ID of movie</param>
        /// <returns>Ok response with a list of all movie ratings for requested movie, or BadRequest with error message</returns>
        [HttpGet]
        [Route("movies/{id}")]
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

        /// <summary>
        /// Gets ratings of user
        /// </summary>
        /// <param name="id">ID of user</param>
        /// <returns>Ok response with a list of all movie ratings for requested user, or BadRequest with error message</returns>
        [HttpGet]
        [Route("users/{id}")]
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

        /// <summary>
        /// Gets movie rating of requested movie by requested user
        /// </summary>
        /// <param name="userId">ID of user</param>
        /// <param name="movieId">ID of movie</param>
        /// <returns>Ok response with a movie rating for requested user and movie, or BadRequest with error message</returns>
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

        /// <summary>
        /// Creates a new movie rating for requested movie
        /// </summary>
        /// <param name="movieRating">Movie rating object</param>
        /// <returns>Empty Ok response, or BadRequest with error message</returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(MovieRatingViewModel movieRating)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();
            movieRating.UserCreated = new UserViewModel() { Id = loggedUserId };
            //movieRating.Movie = new MovieViewModel() { MovieId = movieId };
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