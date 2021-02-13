using AngularJsSample.Api.Mapping.Users;
using AngularJsSample.Api.Models.Movies;
using AngularJsSample.Services.Messaging.Views.Movies;
using System.Collections.Generic;
using System.Linq;

namespace AngularJsSample.Api.Mapping.Movies
{
    public static class MoviesMapper
    {
        public static MovieViewModel MapToViewModel(this Movie view)
        {
            if (view == null) return null;
            return new MovieViewModel()
            {
                MovieId = view.MovieId,
                UserCreated = view.UserCreated.MapToViewModel(),
                DateCreated = view.DateCreated,
                UserLastModified = view.UserLastModified.MapToViewModel(),
                DateLastModified = view.DateLastModified,
                MovieName = view.MovieName,
                MovieDescription = view.MovieDescription,
                MovieReleaseDate = view.MovieReleaseDate,
                MovieRating = view.MovieRating,
                MoviePosterUrl = view.MoviePosterUrl,
                MovieImdbUrl = view.MovieImdbUrl
            };
        }
        public static List<MovieViewModel> MapToViewModels (this IEnumerable<Movie> views)
        {
            var result = new List<MovieViewModel>();
            if (views == null) return result;
            result.AddRange(views.Select(item => item.MapToViewModel()));
            return result;
        }

        public static Movie MapToView (this MovieViewModel viewModel)
        {
            if (viewModel == null) return null;
            return new Movie()
            {
                MovieId = viewModel.MovieId,
                UserCreated = viewModel.UserCreated.MapToView(),
                DateCreated = viewModel.DateCreated,
                UserLastModified = viewModel.UserLastModified.MapToView(),
                DateLastModified = viewModel.DateLastModified,
                MovieName = viewModel.MovieName,
                MovieDescription = viewModel.MovieDescription,
                MovieReleaseDate = viewModel.MovieReleaseDate,
                MovieRating = viewModel.MovieRating,
                MoviePosterUrl = viewModel.MoviePosterUrl,
                MovieImdbUrl = viewModel.MovieImdbUrl
            };
        }
    }
}