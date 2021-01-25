using AutoMapper;
using AngularJsSample.Services.Messaging.Views.Users;

namespace AngularJsSample.Services.Mapping
{
    public static class UsersMapper
    {
        public static UserInfo MapToView(this Model.Users.UserInfo model)
        {
            return Mapper.Map<UserInfo>(model);
        }

        public static Model.Users.UserInfo MapToModel(this UserInfo view)
        {
            if (view == null)
                return null;
            return new Model.Users.UserInfo()
            {
                Id = view.Id,
                Email = view.Email,
                FirstName = view.FirstName,
                LastName = view.LastName
            };
        }

    }
}
