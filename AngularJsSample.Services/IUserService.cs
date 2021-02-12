using AngularJsSample.Services.Messaging.Users;

namespace AngularJsSample.Services
{
    public interface IUserService
    {
        GetUserInfoResponse GetUserInfo(GetUserInfoRequest request);
    }
}