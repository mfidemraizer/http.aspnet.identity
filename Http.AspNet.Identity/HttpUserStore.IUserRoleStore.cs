namespace Http.AspNet.Identity
{
    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    public partial class HttpUserStore<TUser> : IUserRoleStore<TUser>
    {
        public Task AddToRoleAsync(TUser user, string roleName)
            => HttpClient.PutAsJsonAsync
            (
                UserRoleResourceUri.Replace
                (
                    "{userId}",
                    ((IIdentityUser)user).Id
                ),
                roleName
            );

        public async Task<IList<string>> GetRolesAsync(TUser user)
        {
            HttpResponseMessage response = await HttpClient.GetAsync
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

            HttpResponseMessage response = await HttpClient.SendAsync(request);

            return response.IsSuccessStatusCode;
        }

        public Task RemoveFromRoleAsync(TUser user, string roleName)
        {
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

            return HttpClient.SendAsync(request);
        }
    }
}