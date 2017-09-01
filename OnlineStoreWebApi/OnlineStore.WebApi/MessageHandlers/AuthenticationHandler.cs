using OnlineStore.Business.Contracts;
using OnlineStore.Business.DependencyResolvers.Ninject;
using OnlineStore.Core.CrossCuttingConcerns.Security;
using OnlineStore.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace OnlineStore.WebApi.MessageHandlers
{
    public class AuthenticationHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var token = request.Headers.GetValues("Authorization").FirstOrDefault();

                if (token != null)
                {
                    byte[] data = Convert.FromBase64String(token);
                    string decodeKey = Encoding.UTF8.GetString(data);

                    string[] tokenValues = decodeKey.Split(':');
                    var email = tokenValues[0];
                    var password = tokenValues[1];

                    IUserService userService = InstanceFactory.GetInstance<IUserService>();

                    var user = userService.Get(_ => _.Email == email && _.Password == password);
                    if (user != null)
                    {
                        var roles = userService.GetUserRoles(user);

                        CustomIdentity<User> identity = new CustomIdentity<User>
                        {
                            IsAuthenticated = true,
                            Name = user.FullName,
                            UserData = user,
                            Roles = roles
                        };


                        IPrincipal principal = new GenericPrincipal(identity, roles);
                        Thread.CurrentPrincipal = principal;
                        HttpContext.Current.User = principal;
                    }


                }

            }
            catch (Exception)
            {

                throw;
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}