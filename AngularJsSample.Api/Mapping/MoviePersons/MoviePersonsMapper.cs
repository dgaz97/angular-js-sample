using AngularJsSample.Api.Mapping.Users;
using AngularJsSample.Api.Models;
using AngularJsSample.Services.Messaging.Views.MoviePersons;
using System.Collections.Generic;
using System.Linq;

namespace AngularJsSample.Api.Mapping.MoviePersons
{
    /// <summary>
    /// Static class for mapping between MoviePerson View and View Model
    /// </summary>
    public static class MoviePersonsMapper
    {
        /// <summary>
        /// Maps MoviePerson View to View Model
        /// </summary>
        /// <param name="view">MoviePerson View</param>
        /// <returns>MoviePerson ViewModel</returns>
        public static MoviePersonViewModel MapToViewModel(this MoviePerson view)
        {
            if (view == null) return null;
            return new MoviePersonViewModel()
            {
                Id = view.Id,
                UserCreated = view.UserCreated.MapToViewModel(),
                DateCreated = view.DateCreated,
                UserLastModified = view.UserLastModified.MapToViewModel(),
                DateLastModified = view.DateLastModified,
                FirstName = view.FirstName,
                LastName = view.LastName,
                BirthDate = view.BirthDate,
                BirthPlace = view.BirthPlace,
                Biography = view.Biography,
                ImageUrl = view.ImageUrl,
                ImdbUrl = view.ImdbUrl,
                Popularity = view.Popularity
            };
        }
        /// <summary>
        /// Maps MoviePerson View Model to MoviePerson View
        /// </summary>
        /// <param name="viewModel">MoviePerson View Model</param>
        /// <returns>MoviePerson View</returns>
        public static MoviePerson MapToView(this MoviePersonViewModel viewModel)
        {
            if (viewModel == null) return null;
            return new MoviePerson()
            {
                Id = viewModel.Id,
                UserCreated = viewModel.UserCreated.MapToView(),
                DateCreated = viewModel.DateCreated,
                UserLastModified = viewModel.UserLastModified.MapToView(),
                DateLastModified = viewModel.DateLastModified,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                BirthDate = viewModel.BirthDate,
                BirthPlace = viewModel.BirthPlace,
                Biography = viewModel.Biography,
                ImageUrl = viewModel.ImageUrl,
                ImdbUrl = viewModel.ImdbUrl,
                Popularity = viewModel.Popularity
            };
        }
        /// <summary>
        /// Maps multiple MoviePerson Views to a List of MoviePerson ViewModels
        /// </summary>
        /// <param name="views">IEnumerable of MoviePerson Views</param>
        /// <returns>List of MoviePerson View Models</returns>
        public static List<MoviePersonViewModel> MapToViewModels(this IEnumerable<MoviePerson> views)
        {
            var result = new List<MoviePersonViewModel>();
            if (views == null) return result;
            result.AddRange(views.Select(item => item.MapToViewModel()));
            return result;
        }
    }
}