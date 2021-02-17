using AngularJsSample.Api.Mapping.Movies;
using AngularJsSample.Services;
using AngularJsSample.Api.Helpers;
using AngularJsSample.Services.Messaging.Movies.Requests;
using System;
using System.Web;
using System.Web.Http;
using AngularJsSample.Api.Models.Movies;
using AngularJsSample.Api.Mapping.Genres;

namespace AngularJsSample.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/movies")]
    public class MovieController : ApiController
    {
        private IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /**
         * Dohvaća sve filmove
         */
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
            moviesWithGenresViewModels.ForEach(x =>
            {
                var req2 = new FindMovieGenresRequest()
                {
                    RequestToken = Guid.NewGuid(),
                    UserId = loggedUserId,
                    MovieId = x.MovieId
                };
                var res2 = _movieService.FindMovieGenres(req2);
                x.Genres = res2.Genres.MapToViewModels();
            });
            return Ok(new
            {
                movies = moviesWithGenresViewModels
            });

        }


        /**
         * Dohvaća jedan film
         */
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

            var req2 = new FindMovieGenresRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                MovieId = movieWithGenresViewModel.MovieId
            };
            var res2 = _movieService.FindMovieGenres(req2);

            movieWithGenresViewModel.Genres = res2.Genres.MapToViewModels();


            return Ok(movieWithGenresViewModel);

        }

        /**
         * Dodaje novi film
         */
        [HttpPost]
        [Route("")]
        public IHttpActionResult Put(MovieViewModel movie)
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

        /**
         * Ažurira postojeći film
         */
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Put(int id, MovieViewModel movie)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            movie.UserLastModified = new Models.Users.UserViewModel() { Id = loggedUserId };
            movie.MovieId = id;
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

        /**
         * Briše film
         */
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

        /**
         * Dohvaća žanrove filma
         */
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

        /**
         * Dodaje žanr filmu
         */
        [HttpPut]
        [Route("{movieId}/{genreId}")]
        public IHttpActionResult Put(int movieId, int genreId)
        {
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            var request = new AddGenreToMovieRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId,
                MovieId = movieId,
                GenreId = genreId
            };

            var response = _movieService.AddGenreToMovie(request);

            if (!response.Success)
                return BadRequest(response.Message);
            return Ok();
        }



        /**
         * Briše žanr filma
         */
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



    }
}