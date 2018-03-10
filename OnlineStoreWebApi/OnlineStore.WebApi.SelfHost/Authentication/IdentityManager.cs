using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using OnlineStore.Business.Contracts;
using OnlineStore.Business.DependencyResolvers.Ninject;
using OnlineStore.Core.CrossCuttingConcerns.Security;
using OnlineStore.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.WebApi.SelfHost.Authentication
{
    public class IdentityManager// : BaseClass
    {
        private readonly IUserService _userService;
        public IdentityManager()
        {
            _userService = InstanceFactory.GetInstance<IUserService>();
        }

        public void SetClaimIdentity(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                var user = _userService.Get(_ => _.Email == context.UserName && _.Password == context.Password);
                if (user != null)
                {
                    var roles = _userService.GetUserRoles(user);

                    CustomIdentity<User> customIdentity = new CustomIdentity<User>
                    {
                        AuthenticationType = context.Options.AuthenticationType,
                        IsAuthenticated = true,
                        Name = user.FullName,
                        UserData = user,
                        Roles = roles
                    };

                    var json = JsonConvert.SerializeObject(customIdentity);

                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim("UserData", json));
                    foreach (var role in roles)
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, role));
                    }

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
