using AngularJsSample.Api.Mapping.Movies;
using AngularJsSample.Services;
using AngularJsSample.Api.Helpers;
using AngularJsSample.Services.Messaging.Movies.Requests;
using System;
using System.Web;
using System.Web.Http;
using AngularJsSample.Api.Models.Movies;
using AngularJsSample.Api.Mapping.Genres;
using AngularJsSample.Api.Models.MovieGenres;
using System.Threading.Tasks;
using System.Collections.Generic;
using AngularJsSample.Services.Messaging.MovieRoles.Requests;
using AngularJsSample.Api.Models.MovieRoles;

namespace AngularJsSample.Api.Controllers
{
    /// <summary>
    /// Movie API controller
    /// </summary>
    [Authorize]
    [RoutePrefix("api/movies")]
    public class MovieController : ApiController
    {
        private IMovieService _movieService;
        /// <summary>
        /// Constructor, for use with AutoFac, that accepts an IMovieService implementation
        /// </summary>
        /// <param name="movieService">An IMovieService implementation</param>
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Gets a list of all movies
        /// </summary>
        /// <returns>Ok response with a list of all movies, or BadRequest with error message</returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            var request = new GetAllMoviesRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId
            };


            var response = _movieService.GetAllMovies(request);
            if (!response.Success)
                return BadRequest(response.Message);



            var moviesWithGenresViewModels = response.Movies.MapToViewModels();


            List<MovieViewModel> moviesResult = new List<MovieViewModel>();

            moviesWithGenresViewModels.ForEach(x =>
            {
                
                    var req2 = new FindMovieGenresRequest()
                    {
                        RequestToken = Guid.NewGuid(),
                        UserId = loggedUserId,
                        MovieId = x.MovieId
                    };
                    var res2 = _movieService.FindMovieGenresLight(req2);
                    x.Genres = res2.Genres.MapToViewModels();
                    //x.Genres
                    moviesResult.Add(x);

            });

            //await Task.WhenAll(tasks);


            //foreach (var t in tasks)
            //{
            //    moviesResult.Add(t.Result);
            //}
            return Ok(new
            {
                movies = moviesResult
            });

        }


        /// <summary>
        /// Gets one movie, by ID
        /// </summary>
        /// <param name="id">ID of movie that we're getting</param>
        /// <returns>Ok response with the movie that we're getting, or BadRequest with error message</returns>
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            var request = new GetMovieRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                MovieId = id
            };


            var response = _movieService.GetMovie(request);
            if (!response.Success)
                return BadRequest(response.Message);

            var movieWithGenresViewModel = response.Movie.MapToViewModel();

            //if movie wasn't found
            if (movieWithGenresViewModel == null) return Ok();

            var req2 = new FindMovieGenresRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                MovieId = movieWithGenresViewModel.MovieId
            };
            var res2 = _movieService.FindMovieGenresLight(req2);

            movieWithGenresViewModel.Genres = res2.Genres.MapToViewModels();


            return Ok(movieWithGenresViewModel);

        }

        /// <summary>
        /// Adds a new movie
        /// </summary>
        /// <param name="movie">Movie object</param>
        /// <returns>Ok response with a Movie object (only contains newly created ID), or BadRequest with error message</returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(MovieViewModel movie)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            movie.UserCreated = new Models.Users.UserViewModel() { Id = loggedUserId };
            var request = new SaveMovieRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                Movie = movie.MapToView()
            };

            var response = _movieService.SaveMovie(request);
            if (!response.Success)
                return BadRequest(response.Message);
            return Ok(response.Movie.MapToViewModel());

        }

        /// <summary>
        /// Updates movie with ID
        /// </summary>
        /// <param name="id">ID of movie that we're updating</param>
        /// <param name="movie">Movie object with new data</param>
        /// <returns>Ok response with the movie object, or BadRequest with error message</returns>
        [HttpPut]
        [Route("")]
        public IHttpActionResult Put(MovieViewModel movie)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            movie.UserLastModified = new Models.Users.UserViewModel() { Id = loggedUserId };
            //movie.MovieId = id;
            var request = new SaveMovieRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                Movie = movie.MapToView()
            };
            var response = _movieService.SaveMovie(request);
            if (!response.Success)
                return BadRequest(response.Message);
            return Ok(response.Movie.MapToViewModel());
        }

        /// <summary>
        /// Deletes movie
        /// </summary>
        /// <param name="id">ID of movie that we're deleting</param>
        /// <returns>Empty Ok response, or BadRequest with error message</returns>
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            var request = new DeleteMovieRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                MovieId = id
            };

            var response = _movieService.DeleteMovie(request);
            if (!response.Success)
                return BadRequest(response.Message);
            return Ok();
        }

        /// <summary>
        /// Gets genres of requested
        /// </summary>
        /// <param name="id">ID of movie</param>
        /// <returns>Ok response with a list of genres, or BadRequest with error message</returns>
        [HttpGet]
        [Route("genres/{id}")]
        public IHttpActionResult GetGenres(int id)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();
            var request = new FindMovieGenresRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                MovieId = id
            };

            var response = _movieService.FindMovieGenres(request);
            if (!response.Success)
                return BadRequest(response.Message);
            return Ok(new { response.Genres });
        }

        /// <summary>
        /// Adds genre to movie
        /// </summary>
        /// <param name="movieGenre">MovieGenre object</param>
        /// <returns>Empty Ok response, or BadRequest with error message</returns>
        [HttpPost]
        [Route("genres/")]
        public IHttpActionResult Post(MovieGenreViewModel movieGenre)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            var request = new AddGenreToMovieRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                MovieId = movieGenre.Movie.MovieId,
                GenreId = movieGenre.Genre.GenreId
            };

            var response = _movieService.AddGenreToMovie(request);

            if (!response.Success)
                return BadRequest(response.Message);
            return Ok();
        }



        /// <summary>
        /// Deletes genre from movie
        /// </summary>
        /// <param name="movieId">Movie that we're deleting the genre from</param>
        /// <param name="genreId">Genre that we're deleting from movie</param>
        /// <returns>Empty Ok response, or BadRequest with error message</returns>
        [HttpDelete]
        [Route("{movieId}/{genreId}")]
        public IHttpActionResult Delete(int movieId, int genreId)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            var request = new DeleteGenreFromMovieRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                MovieId = movieId,
                GenreId = genreId
            };

            var response = _movieService.DeleteGenreFromMovie(request);
            if (!response.Success)
                return BadRequest(response.Message);
            return Ok();

        }

        [HttpGet]
        [Route("roles/{id}")]
        public IHttpActionResult GetRoles(int id)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            var request = new FindMovieRolesRequest()
            {
                UserId = loggedUserId,
                RequestToken = Guid.NewGuid(),
                MovieId = id
            };

            var response = _movieService.FindMovieRoles(request);

            if (!response.Success)
                return BadRequest(response.Message);
            return Ok(new { movieRoles = response.MovieRoles });

        }

        [HttpPost]
        [Route("roles")]
        public IHttpActionResult PostRole(MovieRoleViewModel movieRole)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            var request = new AddMoviePersonToMovieRequest()
            {
                UserId = loggedUserId,
                RequestToken = Guid.NewGuid(),
                MovieId = movieRole.MovieId.GetValueOrDefault(),
                MovieRoleId = movieRole.MovieRoleId,
                MoviePersonId = movieRole.MoviePersonId.GetValueOrDefault()
            };

            var response = _movieService.AddMoviePersonToMovie(request);

            if (!response.Success)
                return BadRequest(response.Message);
            return Ok();
        }

        [HttpDelete]
        [Route("roles/{movieId}/{movieRoleId}/{moviePersonId}")]
        public IHttpActionResult DeleteRole(int movieId, int movieRoleId, int moviePersonId)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            var request = new DeleteMovieRoleFromMovieRequest()
            {
                UserId = loggedUserId,
                RequestToken = Guid.NewGuid(),
                MovieId = movieId,
                MovieRoleId = movieRoleId,
                MoviePersonId = moviePersonId
            };

            var response = _movieService.DeleteMovieRoleFromMovie(request);

            if (!response.Success)
                return BadRequest(response.Message);
            return Ok();
        }



    }
}