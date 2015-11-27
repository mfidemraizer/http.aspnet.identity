namespace Http.AspNet.Identity
{
    using Microsoft.AspNet.Identity;
    using System.Diagnostics.Contracts;
    using System.Net.Http;
    using System.Threading.Tasks;

    public partial class HttpUserStore<TUser> : IUserTwoFactorStore<TUser, string>
    {
        public async Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Ensures(Contract.Result<Task<bool>>() != null);

            HttpRequestMessage request = new HttpRequestMessage
            (
                HttpMethod.Head,
                UserPropertiesResourceUri.Replace
                (
                    "{userId}",
                    ((IIdentityUser)user).Id
                ).Replace
                (
                    "{settingId}",
                    "twofactorauth.enabled"
                )
            );

            // for example: /users/{userId}/properties/twofactorauth.enabled
            HttpResponseMessage response = await HttpClient.SendAsync(request);

            return response.IsSuccessStatusCode;

        }

        public Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Ensures(Contract.Result<Task>() != null);

            // for example: /users/{userId}/properties/twofactorauth.enabled
            return HttpClient.PutAsJsonAsync
            (
                UserPropertiesResourceUri.Replace("{userId}", ((IIdentityUser)user).Id).Replace("{settingId}", "twofactorenabled"),
                new { enabled = enabled }
            );
        }
    }
}