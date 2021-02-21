using AngularJsSample.Api.Mapping.Users;
using AngularJsSample.Api.Models.Genres;
using AngularJsSample.Services.Messaging.Views.Genres;
using System.Collections.Generic;
using System.Linq;

namespace AngularJsSample.Api.Mapping.Genres
{
    /// <summary>
    /// Static class for mapping between Genre View and View Model
    /// </summary>
    public static class GenresMapper
    {
        /// <summary>
        /// Maps Genre View to View Model
        /// </summary>
        /// <param name="view">Genre View</param>
        /// <returns>Genre ViewModel</returns>
        public static GenreViewModel MapToViewModel(this Genre view)
        {
            if (view == null) return null;
            return new GenreViewModel()
            {
                GenreId = view.GenreId,
                UserCreated = view.UserCreated.MapToViewModel(),
                DateCreated = view.DateCreated,
                UserLastModified = view.UserLastModified.MapToViewModel(),
                DateLastModified = view.DateLastModified,
                Name = view.Name,
                Description = view.Description
            };
        }
        /// <summary>
        /// Maps Genre View Model to MovieRating View
        /// </summary>
        /// <param name="viewModel">Genre View Model</param>
        /// <returns>Genre View</returns>
        public static Genre MapToView(this GenreViewModel viewModel)
        {
            if (viewModel == null) return null;
            return new Genre()
            {
                GenreId = viewModel.GenreId,
                UserCreated = viewModel.UserCreated.MapToView(),
                DateCreated = viewModel.DateCreated,
                UserLastModified = viewModel.UserLastModified.MapToView(),
                DateLastModified = viewModel.DateLastModified,
                Name = viewModel.Name,
                Description = viewModel.Description
            };
        }
        /// <summary>
        /// Maps multiple Genre Views to a List of Genre ViewModels
        /// </summary>
        /// <param name="views">IEnumerable of Genre Views</param>
        /// <returns>List of Genre View Models</returns>
        public static List<GenreViewModel> MapToViewModels(this IEnumerable<Genre> views)
        {
            var result = new List<GenreViewModel>();
            if (views == null) return result;
            result.AddRange(views.Select(item => item.MapToViewModel()));
            return result;
        }
    }
}