using Microsoft.Owin.Security.OAuth;
using OnlineStore.Api.Core;
using OnlineStore.Services.Users;
using System;
using System.Security.Claims;

namespace OnlineStore.Api.Authentication
{
    public class IdentityManager// : BaseClass
    {
        private readonly IUserService _userService;
        public IdentityManager()
        {
            var instance = DependencyContainer.Resolve(typeof(IUserService));
            _userService = (IUserService)instance;
        }

        public void SetClaimIdentity(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                var user = _userService.Get(_ => _.Email == context.UserName && _.Password == context.Password);
                if (user != null)
                {
                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim("Name", user.FullName));
                    identity.AddClaim(new Claim("Email", user.Email));
                    identity.AddClaim(new Claim(ClaimTypes.Role, user.Role));
                    context.Validated(identity);
                }
                else
                {
                    context.SetError("invalid_grant", "Invalid username and password");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}