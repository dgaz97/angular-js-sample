using System.Web;
using System.Web.Http;
using System;
using AngularJsSample.Services;
using AngularJsSample.Api.Helpers;
using AngularJsSample.Api.Mapping.Users;
using AngularJsSample.Services.Messaging.Users;

namespace AngularJsSample.Api.Controllers
{
    /// <summary>
    /// User API controller
    /// </summary>
    [Authorize]
    public class UserInfoController : ApiController
    {
        private readonly IUserService _userService;
        /// <summary>
        /// Constructor, for use with AutoFac, that accepts an IUserService implementation
        /// </summary>
        /// <param name="userService">IUserService implementation</param>
        public UserInfoController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Gets requested user
        /// </summary>
        /// <param name="id">ID of user</param>
        /// <returns>Ok response with the requested user, or BadRequest with error message</returns>
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

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>Ok response with a list of users, or BadRequest with error message</returns>
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
