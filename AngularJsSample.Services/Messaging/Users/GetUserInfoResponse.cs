using AngularJsSample.Services.Messaging.Views.Users;

namespace AngularJsSample.Services.Messaging.Users
{
    public class GetUserInfoResponse:ResponseBase<GetUserInfoRequest>
    {
        public UserInfo User { get; set; }
    }
}
