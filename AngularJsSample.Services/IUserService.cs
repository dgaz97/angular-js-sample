using AngularJsSample.Services.Messaging.Users;
using AngularJsSample.Services.Messaging.Views.Users;

namespace AngularJsSample.Services
{
    public interface IUserService
    {
        GetUserInfoResponse GetUserInfo(GetUserInfoRequest request);
    }
}