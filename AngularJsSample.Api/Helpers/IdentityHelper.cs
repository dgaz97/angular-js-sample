using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;

namespace AngularJsSample.Api.Helpers
{
    public static class IdentityHelper
    {
        public static int GetUserId(this IOwinContext context)
        {
            var authUser = context.Authentication.User;
            var userclaim = authUser.Claims.FirstOrDefault(claim => claim.Type == "userId");
            if (userclaim == null)
                return 0;
            else
            {
                return int.Parse(userclaim.Value);
            }
        }
    }
}