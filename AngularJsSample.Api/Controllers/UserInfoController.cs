using System.Web;
using System.Web.Http;
using System;
using AngularJsSample.Services;
using AngularJsSample.Api.Helpers;
using AngularJsSample.Api.Mapping.Users;
using AngularJsSample.Services.Messaging.Users;

namespace AngularJsSample.Api.Controllers
{
    [Authorize]
    public class UserInfoController : ApiController
    {
        private readonly IUserService _userService;

        public UserInfoController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var request = new GetUserInfoRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = id
            };

            var response = _userService.GetUserInfo(request);

            if (response.Success)
            {
                return Ok(response.User.MapToViewModel());
            }

            return BadRequest(response.Message);
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            //retrieve userid from Bearer token
            var loggedUserId = HttpContext.Current.GetOwinContext().GetUserId();

            var request = new GetUserInfoRequest()
            {
                RequestToken = Guid.NewGuid(),
                UserId = loggedUserId
            };

            var response = _userService.GetUserInfo(request);

            if (response.Success)
            {
                return Ok(response.User.MapToViewModel());
            }

            return BadRequest(response.Message);
        }
    }
}
