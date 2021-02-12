using AngularJsSample.Services.Messaging.Views.Users;
using AngularJsSample.Api.Models.Users;

namespace AngularJsSample.Api.Mapping.Users
{
    public static class UsersMapper
    {
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