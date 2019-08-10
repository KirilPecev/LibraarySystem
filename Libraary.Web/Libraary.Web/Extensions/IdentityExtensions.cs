namespace Libraary.Web.Extensions
{
    using Services;
    using System.Security.Principal;

    public static class IdentityExtensions
    {
        public static IUserService userService { get; set; }

        public static string GetFirstName(this IIdentity identity)
        {
            var email = identity.Name;

            var firstName = userService.GetFirstName(email);

            return firstName ?? string.Empty;
        }
    }
}
