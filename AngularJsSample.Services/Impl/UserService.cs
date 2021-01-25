using AngularJsSample.Model.Users;
using AngularJsSample.Services.Mapping;
using AngularJsSample.Services.Messaging.Users;
using AngularJsSample.Services.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Services.Impl
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public GetUserInfoResponse GetUserInfo(GetUserInfoRequest request)
        {
            var response = new GetUserInfoResponse()
            {
                ResponseToken = Guid.NewGuid(),
                Request = request
            };

            try
            {
                response.User = _repository.FindBy(request.UserId).MapToView();
                response.Success = true;
            }
            catch(Exception)
            {
                response.Success = false;
                response.Message = GenericErrorMessageFactory.GeneralError;
            }

            return response;
        }
    }
}
