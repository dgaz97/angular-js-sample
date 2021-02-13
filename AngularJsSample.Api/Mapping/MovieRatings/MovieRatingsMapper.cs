using AngularJsSample.Api.Mapping.Movies;
using AngularJsSample.Api.Mapping.Users;
using AngularJsSample.Api.Models.MovieRatings;
using AngularJsSample.Services.Messaging.Views.MovieRatings;
using System.Collections.Generic;
using System.Linq;

namespace AngularJsSample.Api.Mapping.MovieRatings
{
    public static class MovieRatingsMapper
    {
        public static MovieRatingViewModel MapToViewModel(this MovieRating view)
        {
            if (view == null) return null;
            return new MovieRatingViewModel()
            {
                UserCreated = view.UserCreated.MapToViewModel(),
                Movie = view.Movie.MapToViewModel(),
                DateCreated = view.DateCreated,
                UserRating = view.UserRating
            };
        }
        public static List<MovieRatingViewModel> MapToViewModels(this IEnumerable<MovieRating> views)
        {
            var result = new List<MovieRatingViewModel>();
            if (views == null) return result;
            result.AddRange(views.Select(item => item.MapToViewModel()));
            return result;
        }

        public static MovieRating MapToView(this MovieRatingViewModel viewModel)
        {
            if (viewModel == null) return null;
            return new MovieRating()
            {
                UserCreated = viewModel.UserCreated.MapToView(),
                Movie = viewModel.Movie.MapToView(),
                DateCreated = viewModel.DateCreated,
                UserRating = viewModel.UserRating
            };

        }
    }
}