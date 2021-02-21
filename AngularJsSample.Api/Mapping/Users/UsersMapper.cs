using AngularJsSample.Services.Messaging.Views.Users;
using AngularJsSample.Api.Models.Users;

namespace AngularJsSample.Api.Mapping.Users
{
    /// <summary>
    /// Static class for mapping between User View and View Model
    /// </summary>
    public static class UsersMapper
    {
        /// <summary>
        /// Maps User View to User ViewModel
        /// </summary>
        /// <param name="view">User View</param>
        /// <returns>User View Model</returns>
        public static UserViewModel MapToViewModel(this UserInfo view)
        {
            if (view == null)
                return null;
            return new UserViewModel()
            {
                Id = view.Id,
                Email = view.Email,
                Firstname = view.FirstName,
                Lastname = view.LastName,
                FullName = view.FullName
            };
        }

        /// <summary>
        /// Maps User ViewModel to View
        /// </summary>
        /// <param name="viewModel">User ViewModel</param>
        /// <returns>User View</returns>
        public static UserInfo MapToView(this UserViewModel viewModel)
        {
            if (viewModel == null)
                return null;
            return new UserInfo()
            {
                Id = viewModel.Id,
                Email = viewModel.Email,
                FirstName = viewModel.Firstname,
                LastName = viewModel.Lastname,
                FullName = viewModel.FullName
            };
        }
    }
}