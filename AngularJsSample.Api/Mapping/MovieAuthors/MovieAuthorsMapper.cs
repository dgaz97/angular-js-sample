using AngularJsSample.Api.Mapping.Users;
using AngularJsSample.Api.Models;
using AngularJsSample.Services.Messaging.Views.MovieAuthors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJsSample.Api.Mapping.MovieAuthors
{
    public static class MovieAuthorsMapper
    {
        public static MovieAuthorViewModel MapToViewModel(this MovieAuthor view)
        {
            if (view == null) return null;
            return new MovieAuthorViewModel()
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
        public static MovieAuthor MapToView(this MovieAuthorViewModel viewModel)
        {
            if (viewModel == null) return null;
            return new MovieAuthor()
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

        public static List<MovieAuthorViewModel> MapToViewModels(this IEnumerable<MovieAuthor> views)
        {
            var result = new List<MovieAuthorViewModel>();
            if (views == null) return result;
            result.AddRange(views.Select(item => item.MapToViewModel()));
            return result;
        }
    }
}