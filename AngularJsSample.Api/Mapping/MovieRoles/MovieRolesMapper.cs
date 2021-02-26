using AngularJsSample.Api.Mapping.Users;
using AngularJsSample.Api.Models.MovieRoles;
using AngularJsSample.Services.Messaging.Views.MovieRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJsSample.Api.Mapping.MovieRoles
{
    public static class MovieRolesMapper
    {
        public static MovieRoleViewModel MapToViewModel(this MovieRole view)
        {
            if (view == null) return null;
            return new MovieRoleViewModel()
            {
                MovieRoleId = view.MovieRoleId,
                MovieRoleName = view.MovieRoleName,
                MovieRoleDescription = view.MovieRoleDescription,
                DateCreated = view.DateCreated,
                UserCreated = view.UserCreated.MapToViewModel(),
                DateLastModified = view.DateLastModified,
                UserLastModified = view.UserLastModified.MapToViewModel(),
                MovieId = view.MovieId,
                MovieName = view.MovieName,
                MoviePersonId = view.MoviePersonId,
                MoviePersonName = view.MoviePersonName
            };
        }

        public static MovieRole MapToView(this MovieRoleViewModel viewModel)
        {
            if (viewModel == null) return null;
            return new MovieRole()
            {
                MovieRoleId = viewModel.MovieRoleId,
                MovieRoleName = viewModel.MovieRoleName,
                MovieRoleDescription = viewModel.MovieRoleDescription,
                DateCreated = viewModel.DateCreated,
                UserCreated = viewModel.UserCreated.MapToView(),
                DateLastModified = viewModel.DateLastModified,
                UserLastModified = viewModel.UserLastModified.MapToView(),
                MovieId = viewModel.MovieId,
                MovieName = viewModel.MovieName,
                MoviePersonId = viewModel.MoviePersonId,
                MoviePersonName = viewModel.MoviePersonName
            };
        }

        public static List<MovieRoleViewModel> MapToViewModels(this IEnumerable<MovieRole> views)
        {
            var result = new List<MovieRoleViewModel>();
            if (views == null) return result;
            result.AddRange(views.Select(item => item.MapToViewModel()));
            return result;
        }
    }
}