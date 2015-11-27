namespace Http.AspNet.Identity
{
    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Net.Http;
    using System.Threading.Tasks;

    public partial class HttpUserStore<TUser> : IUserRoleStore<TUser>
    {
        public Task AddToRoleAsync(TUser user, string roleName)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Requires(!string.IsNullOrEmpty(roleName), "Given role name must not be null or empty");
            Contract.Ensures(Contract.Result<Task>() != null);

            return HttpClient.PutAsJsonAsync // for example: /users/{userId}/roles
            (
                UserRoleResourceUri.Replace
                (
                    "{userId}",
                    ((IIdentityUser)user).Id
                ),
                new { roleName = roleName }
            );
        }

        public async Task<IList<string>> GetRolesAsync(TUser user)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Ensures(Contract.Result<Task<IList<string>>>() != null);

            HttpResponseMessage response = await HttpClient.GetAsync // for example: /users/{userId}/roles
            (
                UserRoleResourceUri.Replace
                (
                    "{userId}",
                    ((IIdentityUser)user).Id
                )
            );

            return await response.Content.ReadAsAsync<IList<string>>();
        }

        public async Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Requires(!string.IsNullOrEmpty(roleName), "Given role name must not be null or empty");
            Contract.Ensures(Contract.Result<Task<bool>>() != null);

            HttpRequestMessage request = new HttpRequestMessage
            (
                HttpMethod.Head,
                UserRoleResourceUri.Replace
                (
                    "{userId}",
                    ((IIdentityUser)user).Id
                ).Replace
                (
                    "{roleName}",
                    roleName
                )
            );

            // for example: /users/{userId}/roles/{roleName}
            HttpResponseMessage response = await HttpClient.SendAsync(request);

            return response.IsSuccessStatusCode;
        }


        public Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            Contract.Requires(user != null, "Given user must be a non-null reference");
            Contract.Requires(!string.IsNullOrEmpty(roleName), "Given role name must not be null or empty");
            Contract.Ensures(Contract.Result<Task>() != null);

            HttpRequestMessage request = new HttpRequestMessage
            (
                HttpMethod.Delete,
                UserRoleResourceUri.Replace
                (
                    "{userId}",
                    ((IIdentityUser)user).Id
                ).Replace
                (
                    "{roleName}",
                    roleName
                )
            );

            // for example: /users/{userId}/roles/{roleName}
            return HttpClient.SendAsync(request);
        }
    }
}