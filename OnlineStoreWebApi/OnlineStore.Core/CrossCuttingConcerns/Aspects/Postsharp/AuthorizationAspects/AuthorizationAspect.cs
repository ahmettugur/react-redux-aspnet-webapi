using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineStore.Core.CrossCuttingConcerns.Aspects.Postsharp.AuthorizationAspects
{
    [Serializable]
    public class AuthorizationAspect : OnMethodBoundaryAspect
    {
        public string Roles { get; set; }


        public override void OnEntry(MethodExecutionArgs args)
        {
            if (string.IsNullOrEmpty(Roles))
            {
                throw new Exception("Invalid roles");
            }

            var roles = Roles.Split(',');
            bool isAuthorize = false;

            foreach (var role in roles)
            {
                if (Thread.CurrentPrincipal.IsInRole(role))
                {
                    isAuthorize = true;
                }
            }

            if (isAuthorize == false)
            {
                throw new SecurityException("You are not authorized.");
            }

            base.OnEntry(args);
        }

    }
}
