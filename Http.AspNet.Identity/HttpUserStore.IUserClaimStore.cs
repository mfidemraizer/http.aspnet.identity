namespace Http.AspNet.Identity
{
    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public partial class HttpUserStore<TUser> : IUserClaimStore<TUser>
    {
        public virtual Task AddClaimAsync(TUser user, Claim claim)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Requires(claim != null, "Given claim must be a non-null reference");
            Contract.Ensures(Contract.Result<Task>() != null);

            // for example: /users/{userId}/claims
            return HttpClient.PostAsJsonAsync
            (
                UserClaimStoreResourceUri.Replace("{userId}", ((IIdentityUser)user).Id),
                claim
            );
        }

        public virtual async Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Ensures(Contract.Result<Task<IList<Claim>>>() != null);

            // for example: /users/{userId}/claims
            HttpResponseMessage response = await HttpClient.GetAsync
            (
                UserClaimStoreResourceUri.Replace("{userId}", ((IIdentityUser)user).Id)
            );

            return await response.Content.ReadAsAsync<IList<Claim>>();
        }

        public virtual Task RemoveClaimAsync(TUser user, Claim claim)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Requires(claim != null, "Given claim must be a non-null reference");
            Contract.Ensures(Contract.Result<Task>() != null);

            // for example: /users/{userId}/logins/{providerKey}
            HttpRequestMessage request = new HttpRequestMessage
            (
                HttpMethod.Delete,
                $"{UserClaimStoreResourceUri.Replace("{userId}", ((IIdentityUser)user).Id)}/{claim.Type}"
            );

            return HttpClient.SendAsync(request);
        }
    }
}
