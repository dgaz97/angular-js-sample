using AngularJsSample.Api.Mapping.Movies;
using AngularJsSample.Api.Mapping.Users;
using AngularJsSample.Api.Models.MovieRatings;
using AngularJsSample.Services.Messaging.Views.MovieRatings;
using System.Collections.Generic;
using System.Linq;

namespace AngularJsSample.Api.Mapping.MovieRatings
{
    /// <summary>
    /// Static class for mapping between MovieRating View and View Model
    /// </summary>
    public static class MovieRatingsMapper
    {
        /// <summary>
        /// Maps MovieRating View to View Model
        /// </summary>
        /// <param name="view">MovieRating View</param>
        /// <returns>MovieRating ViewModel</returns>
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
        /// <summary>
        /// Maps multiple MovieRating Views to a List of MovieRating ViewModels
        /// </summary>
        /// <param name="views">IEnumerable of MovieRating Views</param>
        /// <returns>List of MovieRating View Models</returns>
        public static List<MovieRatingViewModel> MapToViewModels(this IEnumerable<MovieRating> views)
        {
            var result = new List<MovieRatingViewModel>();
            if (views == null) return result;
            result.AddRange(views.Select(item => item.MapToViewModel()));
            return result;
        }

        /// <summary>
        /// Maps MovieRating View Model to MovieRating View
        /// </summary>
        /// <param name="viewModel">MovieRating View Model</param>
        /// <returns>MovieRating View</returns>
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