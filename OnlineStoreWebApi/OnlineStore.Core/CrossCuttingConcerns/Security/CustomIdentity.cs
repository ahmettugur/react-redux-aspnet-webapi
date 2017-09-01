using System.Security.Principal;

namespace OnlineStore.Core.CrossCuttingConcerns.Security
{
    public class CustomIdentity<T> : IIdentity where T : class, new()

    {
        public string Name { get; set; }

        public string AuthenticationType { get; set; }

        public bool IsAuthenticated { get; set; }
        public T UserData { get; set; }
        public string[] Roles { get; set; }

    }
}
