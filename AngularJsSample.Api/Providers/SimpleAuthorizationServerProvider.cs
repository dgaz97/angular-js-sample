using System.Security.Claims;
using System.Threading.Tasks;
using AngularJsSample.Authentication;
using Microsoft.Owin.Security.OAuth;

namespace AngularJsSample.Api.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.CompletedTask;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
 
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            MyUser user = null;
            using (AuthRepository _repo = new AuthRepository())
            {
                user = await _repo.FindUserAsync(context.UserName, context.Password);
 
                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
            }
 
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("userId",user.Id.ToString()));
            context.Validated(identity);
 
        }
    }
}